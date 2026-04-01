using FiscalServerApi;
using FiscalServerApi.ExceptionServer;
using ShopProject.Helpers;
using ShopProject.Model.Domain.Operation;
using ShopProject.Model.Domain.Order;
using ShopProject.Model.Domain.Product;
using ShopProject.Model.Domain.Setting;
using ShopProject.Model.Domain.SignatureKey;
using ShopProject.Model.Domain.WorkingShift;
using ShopProject.Model.Enum;
using ShopProject.Services.Integration.File.Xml;
using ShopProject.Services.Integration.Network.FiscalServerApi.Helpers; 
using SigningFileLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Services.Integration.Network.FiscalServerApi
{
    public class MainFiscalServerController
    {
        private SigningFileContoller _signFileContoller;
        private FiscalServerController _fiscalServerController;
        private XmlServise _xmlServise;
        private bool _testMode = false;
        private SignatureKey _key;
        private TypeChek _typeChek;
        private const int Depth = 0;
        private const int MaxDepth = 5;

        public MainFiscalServerController()
        {
            _signFileContoller = new SigningFileContoller();
            _fiscalServerController = new FiscalServerController(); 
            _signFileContoller.Initialize(false);
            _key = new SignatureKey();
            _xmlServise = new XmlServise();
            SetMode();
        }

        private void SetMode()
        { 
            //var operationRecorder = SettingService.GetSetting<OperationRecorderSetting>("OperationRecorder"); 

            //_testMode = operationRecorder.IsTestMode;
        }

        public void AddKey(SignatureKey key)
        {
            if (_key != null) 
            {
                _key = key;
            }
        }

        private string ChoseTypeOperationRecursive(WorkingShift shift, int depth, int maxDepth, Operation? operation = null, List<Order>? orders = null, List<Product>? products = null)
        {
            try
            {
                if (depth >= maxDepth)
                {
                    throw new Exception("Неможливо виконати операцію");
                }
                return SendCheck(shift, operation, orders, products);
            }
            catch (ExceptionBadHashPrev exbadhash)
            {
                switch (_typeChek)
                {
                    case TypeChek.OpenShift:
                        {
                            shift.MACCreateAt.Content = exbadhash.Error.Split(" ")[3];
                            break;
                        }
                    case TypeChek.CloseShift:
                        {
                            shift.MACEndAt.Content = exbadhash.Error.Split(" ")[3];
                            break;
                        }
                    default:
                        {
                            operation.MAC.Content = exbadhash.Error.Split(" ")[3];
                            break;
                        }
                }
                return ChoseTypeOperationRecursive(shift, depth, maxDepth, operation, orders, products);
            }
            catch (ExceptionCheck exCheck)
            {
                MessageBox.Show(exCheck.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return string.Empty;
            }
            catch (ExceptionSave exSave)
            {
                MessageBox.Show(exSave.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return string.Empty;
            }
            catch (ExceptionCheckShiftIsNotOpen)
            {
                throw;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return string.Empty;
            }
        }
        public string OpenShift(WorkingShift shift)
        {
            _typeChek = TypeChek.OpenShift;
            return ChoseTypeOperationRecursive(shift, Depth, MaxDepth);
        }
        public string CloseShift(WorkingShift shift)
        {
            _typeChek = TypeChek.CloseShift;
            return ChoseTypeOperationRecursive(shift, Depth, MaxDepth); 
        }

        public string DepositAndWithdrawalMoney(WorkingShift shift , Operation operation)
        {
            _typeChek = TypeChek.DepositAndWithdrawalMoney;
            return ChoseTypeOperationRecursive(shift, Depth, MaxDepth , operation);

        }

        public string SendFiscalCheck(WorkingShift shift, Operation operation, List<Product> products)
        {
            _typeChek = TypeChek.FiscalCheck;
            return ChoseTypeOperationRecursive(shift, Depth, MaxDepth, operation, null, products);

        }
        public string SendReturnFiscalCheck(WorkingShift shift, Operation operation, List<Product> products)
        {
            _typeChek = TypeChek.ReturnCheck;
            return ChoseTypeOperationRecursive(shift, Depth, MaxDepth, operation, null, products);

        }

        private string SendCheck(WorkingShift shift, Operation operation, List<Order>? orders, List<Product>? products)
        { 
            string result = string.Empty;
            switch (_typeChek)
            {
                case TypeChek.OpenShift:
                    {
                        _xmlServise.CreateXMLFileOpenShift(shift);
                        if (_signFileContoller.SignFileToByteKey(_key.Signature, _key.SignaturePassword))
                        {
                            result = _fiscalServerController.SendServiceCheck(long.Parse(shift.CreateAt.ToString("yyyyMMddHHmmss")),
                            Convert.ToInt32(0), shift.FiscalNumberRRO, _testMode);
                            if (result != string.Empty)
                            {
                                result = "OK";
                            }
                        } 
                        break;
                    }
                case TypeChek.CloseShift:
                    {
                        _xmlServise.CreateXMLFileCloseShift(shift);
                        if (_signFileContoller.SignFileToByteKey(_key.Signature, _key.SignaturePassword))
                        {
                            result = _fiscalServerController.SendZReport(long.Parse(shift.EndAt.ToString("yyyyMMddHHmmss")),
                            Convert.ToInt32(shift.TotalCheckForShift + 1), shift.FiscalNumberRRO, _testMode);
                            if (result != string.Empty)
                            {
                                result = "OK";
                            }
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
                        if (_signFileContoller.SignFileToByteKey(_key.Signature, _key.SignaturePassword))
                        { 
                            result = _fiscalServerController.SendServiceCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
                            Convert.ToInt32(operation.NumberPayment), shift.FiscalNumberRRO, _testMode);
                            if (result != string.Empty)
                            {
                                result = "OK";
                            }
                        }
                        break;
                    }
                case TypeChek.FiscalCheck:
                    {

                        _xmlServise.CreateXMLFileFiscalCheck(shift, operation, products);
                        if (_signFileContoller.SignFileToByteKey(_key.Signature, _key.SignaturePassword))
                        {
                            result = _fiscalServerController.SendFiscalCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
                            Convert.ToInt32(operation.NumberPayment), shift.FiscalNumberRRO, _testMode);
                        }
                        break;
                    }
                case TypeChek.ReturnCheck:
                    {
                        _xmlServise.CreateXMLFileFiscalCheck(shift, operation, products);
                        if (_signFileContoller.SignFileToByteKey(_key.Signature, _key.SignaturePassword))
                        {
                            result = _fiscalServerController.SendFiscalCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
                            Convert.ToInt32(operation.NumberPayment), shift.FiscalNumberRRO, _testMode);
                        }
                        break;
                    }
            }
            return result;  
        }  
    }
}
