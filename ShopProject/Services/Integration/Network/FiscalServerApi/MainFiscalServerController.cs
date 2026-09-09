using FiscalServerApi;
using FiscalServerApi.ExceptionServer; 
using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Domain.Order;
using ShopProject.Model.Domain.Product; 
using ShopProject.Model.Domain.SignatureKey;
using ShopProject.Model.Domain.WorkingShift;
using ShopProject.Model.Enum;
using ShopProject.Services.Integration.File.Xml;
using ShopProject.Services.Integration.Network.FiscalServerApi.Helpers;
using ShopProject.Services.Modules.Common;
using SigningFileLib;
using System;
using ShopProject.Services.Modules.Mapping.OperaionResult;
using System.Collections.Generic; 
using System.Windows;

namespace ShopProject.Services.Integration.Network.FiscalServerApi
{
    public class MainFiscalServerController
    {
        private SigningFileContoller _signFileContoller;
        private FiscalServerController _fiscalServerController;
        private XmlServise _xmlServise; 
        private SignatureKey _key;
        private TypeChek _typeChek;
        private const int Depth = 0;
        private const int MaxDepth = 5;

        public MainFiscalServerController(SignatureKey key)
        {
            _signFileContoller = new SigningFileContoller();
            _fiscalServerController = new FiscalServerController(); 
            _signFileContoller.Initialize(false);
            _key = key;
            _xmlServise = new XmlServise(); 
        }  
        public void AddKey(SignatureKey key)
        {
            _key = key;
        }

        private OperationResult<string> ChoseTypeOperationRecursive(WorkingShift shift, int depth, int maxDepth, bool testMode, Operation? operation = null, List<Order>? orders = null, List<Product>? products = null)
        {
            try
            {
                if (depth >= maxDepth)
                {
                    return OperationResult<string>.Fail("Невдалося виконати операцію"); 
                }
                var result = SendCheck(shift, operation, orders, products, testMode);

                if (result.IsSuccess)
                {
                    return result;
                }
                else if(result.IsError && result.ErrorType == Modules.Common.Enum.ErrorType.ErrorBadHashPrev)
                {
                    switch (_typeChek)
                    {
                        case TypeChek.OpenShift:
                            {
                                shift.MACCreateAt.Content = result.Data.Split(" ")[3];
                                break;
                            }
                        case TypeChek.CloseShift:
                            {
                                shift.MACEndAt.Content = result.Data.Split(" ")[3];
                                break;
                            }
                        default:
                            {
                                operation.MAC.Content = result.Data.Split(" ")[3];
                                break;
                            }
                    }
                    return ChoseTypeOperationRecursive(shift, depth, maxDepth, testMode, operation, orders, products);
                }
                else if( result.IsError && result.ErrorType == Modules.Common.Enum.ErrorType.IncorrectHash)
                {

                }
                else
                {
                    return result;
                } 

                return OperationResult<string>.Fail("Невдалося виконати операцію");
            }  
            catch(Exception ex)
            {
                return OperationResult<string>.Fail(ex.Message); 
            }
        }
        public OperationResult<string> OpenShift(WorkingShift shift,bool testMode = true)
        {
            _typeChek = TypeChek.OpenShift;
            return ChoseTypeOperationRecursive(shift, Depth, MaxDepth, testMode);
        }
        public OperationResult<string> CloseShift(WorkingShift shift, bool testMode = true)
        {
            _typeChek = TypeChek.CloseShift;
            return ChoseTypeOperationRecursive(shift, Depth, MaxDepth, testMode); 
        }

        public OperationResult<string> DepositAndWithdrawalMoney(WorkingShift shift , Operation operation, bool testMode = true)
        {
            _typeChek = TypeChek.DepositAndWithdrawalMoney;
            return ChoseTypeOperationRecursive(shift, Depth, MaxDepth , testMode, operation);

        }

        public OperationResult<string> SendFiscalCheck(WorkingShift shift, Operation operation, List<Product> products, bool testMode = true)
        {
            _typeChek = TypeChek.FiscalCheck;
            return ChoseTypeOperationRecursive(shift, Depth, MaxDepth, testMode, operation, null, products);

        }
        public OperationResult<string> SendReturnFiscalCheck(WorkingShift shift, Operation operation, List<Product> products, bool testMode = true)
        {
            _typeChek = TypeChek.ReturnCheck;
            return ChoseTypeOperationRecursive(shift, Depth, MaxDepth, testMode, operation, null, products);

        } 
        private OperationResult<string> SendCheck(WorkingShift shift, Operation operation, List<Order>? orders, List<Product>? products , bool testMode)
        {  
            var result = new OperationResult<string>();
            if(_key == null)
            {
                return OperationResult<string>.Fail("Виникла помилка зчитування ключа перезапустіть програму", Modules.Common.Enum.ErrorType.ErrorKey);
            }
            if(_key.SignaturePassword == null || _key.Signature == null)
            {
                return OperationResult<string>.Fail("Виникла помилка зчитування ключа перезапустіть програму",Modules.Common.Enum.ErrorType.ErrorKey);
            }

            switch (_typeChek)
            {
                case TypeChek.OpenShift:
                    {
                        _xmlServise.CreateXMLFileOpenShift(shift);
                        var resultOperation = _signFileContoller.GetError(_signFileContoller.SignFileToByteKey(_key.Signature, _key.SignaturePassword)); 
                        if (resultOperation.IsSuccess)
                        {
                            result = _fiscalServerController.SendServiceCheck(long.Parse(shift.CreateAt.ToString("yyyyMMddHHmmss")),
                            Convert.ToInt32(0), shift.FiscalNumberRRO, testMode).ToOperationResult();
                        }
                        else
                        {
                            return OperationResult<string>.Fail(resultOperation.ErrorMessage);
                        }
                        break;
                    }
                case TypeChek.CloseShift:
                    {
                        _xmlServise.CreateXMLFileCloseShift(shift);
                        var resultOperation = _signFileContoller.GetError(_signFileContoller.SignFileToByteKey(_key.Signature, _key.SignaturePassword));
                        if (resultOperation.IsSuccess)
                        {
                            result = _fiscalServerController.SendZReport(long.Parse(shift.EndAt.ToString("yyyyMMddHHmmss")),
                            Convert.ToInt32(shift.TotalCheckForShift + 1), "4001337633", testMode).ToOperationResult(); 
                        }
                        else
                        {
                            return OperationResult<string>.Fail(resultOperation.ErrorMessage);
                        }
                        break;
                    }
                case TypeChek.DepositAndWithdrawalMoney:
                    {
                        if(operation.TypeOperation ==  TypeOperation.DepositMoney)
                        {
                            operation.TypeOperation =  TypeOperation.DepositMoney;
                            _xmlServise.CreateXMLFileDepositMoney(shift, operation);
                        }
                        else if(operation.TypeOperation ==  TypeOperation.WithdrawalMoney)
                        {
                            operation.TypeOperation = TypeOperation.WithdrawalMoney;
                            _xmlServise.CreateXMLFileWithdrawalMoney(shift, operation);
                        }
                        var resultOperation = _signFileContoller.GetError(_signFileContoller.SignFileToByteKey(_key.Signature, _key.SignaturePassword));
                        if (resultOperation.IsSuccess)
                        { 
                            result = _fiscalServerController.SendServiceCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
                            Convert.ToInt32(operation.NumberPayment), shift.FiscalNumberRRO, testMode).ToOperationResult(); 
                        }
                        else
                        {
                            return OperationResult<string>.Fail(resultOperation.ErrorMessage);
                        }
                        break;
                    }
                case TypeChek.FiscalCheck:
                    {

                        _xmlServise.CreateXMLFileFiscalCheck(shift, operation, products);
                        var resultOperation = _signFileContoller.GetError(_signFileContoller.SignFileToByteKey(_key.Signature, _key.SignaturePassword));
                        if (resultOperation.IsSuccess)
                        {
                            result = _fiscalServerController.SendFiscalCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
                            Convert.ToInt32(operation.NumberPayment), shift.FiscalNumberRRO, testMode).ToOperationResult();
                        }
                        else
                        {
                            return OperationResult<string>.Fail(resultOperation.ErrorMessage);
                        }
                        break;
                    }
                case TypeChek.ReturnCheck:
                    {
                        _xmlServise.CreateXMLFileFiscalCheck(shift, operation, products);
                        var resultOperation = _signFileContoller.GetError(_signFileContoller.SignFileToByteKey(_key.Signature, _key.SignaturePassword));
                        if (resultOperation.IsSuccess)
                        {
                            result = _fiscalServerController.SendFiscalCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
                            Convert.ToInt32(operation.NumberPayment), shift.FiscalNumberRRO, testMode).ToOperationResult();
                        }
                        else
                        {
                            return OperationResult<string>.Fail(resultOperation.ErrorMessage);
                        }
                        break;
                    }
            }
            return result;  
        }  
    }
}
