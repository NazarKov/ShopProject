using FiscalServerApi;
using FiscalServerApi.ExceptionServer;
using ShopProject.Helpers.NetworkServise.FiscalServerApi.Helpers;
using ShopProject.UIModel;
using ShopProject.UIModel.SalePage;
using ShopProjectSQLDataBase.Entities;
using SigningFileLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Helpers.NetworkServise.FiscalServerApi
{
    public class MainFiscalServerController
    {
        private SigningFileContoller _signFileContoller;
        private FiscalServerController _fiscalServerController;
        private bool _testMode = false;
        private ElectronicSignatureKey _key;
        private TypeChek _typeChek;

        public MainFiscalServerController()
        {
            _signFileContoller = new SigningFileContoller();
            _fiscalServerController = new FiscalServerController();
            _testMode = (bool)AppSettingsManager.GetParameterFiles("TestMode");
            _signFileContoller.Initialize(false);
            _key = new ElectronicSignatureKey();
        }

        public void AddKey(ElectronicSignatureKey key)
        {
            _key = key;
        }

        private string ChoseTypeOperationRecursive(UIWorkingShiftModel shift, int depth, int maxDepth, UIOperationModel? operation = null, List<OrderEntity>? orders = null, List<ProductEntity>? products = null)
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return string.Empty;
            }
        }
        public string OpenShift(UIWorkingShiftModel shift)
        {
            _typeChek = TypeChek.OpenShift;
            return ChoseTypeOperationRecursive(shift, 0, 5);
        }
        public string CloseShift(UIWorkingShiftModel shift)
        {
            _typeChek = TypeChek.CloseShift;
            return ChoseTypeOperationRecursive(shift, 0, 5); 
        }

        public string DepositAndWithdrawalMoney(UIWorkingShiftModel shift)
        {
            _typeChek = TypeChek.DepositAndWithdrawalMoney;
            return ChoseTypeOperationRecursive(shift, 0, 5);

        }

        public string SendFiscalCheck(UIWorkingShiftModel shift, UIOperationModel operation, List<ProductEntity> products)
        {
            _typeChek = TypeChek.FiscalCheck;
            return ChoseTypeOperationRecursive(shift, 0, 5, operation, null, products);

        }
        public string SendReturnFiscalCheck(UIWorkingShiftModel shift, UIOperationModel operation, List<ProductEntity> products)
        {
            _typeChek = TypeChek.ReturnCheck;
            return ChoseTypeOperationRecursive(shift, 0, 5, operation, null, products);

        }

        private string SendCheck(UIWorkingShiftModel shift, UIOperationModel? operation, List<OrderEntity>? orders, List<ProductEntity>? products)
        {
            WriteReadXmlFile.WriteXMLFile(shift, operation, orders, products);

            if (_signFileContoller.SignFileToByteKey(_key.Signature, _key.SignaturePassword))
            {
                string result = string.Empty;
                switch (_typeChek)
                {
                    case TypeChek.OpenShift:
                        {
                            result = _fiscalServerController.SendServiceCheck(long.Parse(shift.CreateAt.ToString("yyyyMMddHHmmss")),
                                Convert.ToInt32(0), shift.FiscalNumberRRO, _testMode);
                            if (result != string.Empty)
                            {
                                result = "OK";
                            }
                            break;
                        }
                    case TypeChek.CloseShift:
                        {
                            result = _fiscalServerController.SendZReport(long.Parse(shift.EndAt.ToString("yyyyMMddHHmmss")),
                                Convert.ToInt32(shift.TotalCheckForShift + 1), shift.FiscalNumberRRO, _testMode);
                            if (result != string.Empty)
                            {
                                result = "OK";
                            }
                            break;
                        }
                    case TypeChek.DepositAndWithdrawalMoney:
                        {
                            result = _fiscalServerController.SendServiceCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
                                Convert.ToInt32(operation.NumberPayment), shift.FiscalNumberRRO, _testMode);
                            break;
                        }
                    case TypeChek.FiscalCheck:
                        {
                            result = _fiscalServerController.SendFiscalCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
                                Convert.ToInt32(operation.NumberPayment), shift.FiscalNumberRRO, _testMode);
                            break;
                        }
                    case TypeChek.ReturnCheck:
                        {
                            result = _fiscalServerController.SendFiscalCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
                                 Convert.ToInt32(operation.NumberPayment), shift.FiscalNumberRRO, _testMode);
                            break;
                        }
                }
                return result;
            }
            else
            {
                throw new Exception("невдалося підписати файл");
            }
        }  
    }
}
