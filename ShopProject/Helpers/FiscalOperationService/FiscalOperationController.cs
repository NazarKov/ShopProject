using FiscalServerApi;
using FiscalServerApi.ExceptionServer;
using GreetClient;
using ShopProject.Helpers.FiscalOperationService.Helpers;
using ShopProject.Helpers.NetworkServise.ShopProjectWebServerApi; 
using ShopProjectSQLDataBase.Entities;
using SigningFileLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows; 

namespace ShopProject.Helpers.FiscalOperationService
{
    public class FiscalOperationController
    {
        private SigningFileContoller _signFileContoller;
        private FiscalServerController _fiscalServerController;
        private readonly string pathxml = "C:\\ProgramData\\ShopProject\\Temp\\Chek.xml";
        private bool _testMode = false;

        public FiscalOperationController()
        {
            _signFileContoller = new SigningFileContoller();
            _fiscalServerController = new FiscalServerController();
            _signFileContoller.Initialize(false);
            //_testMode = (bool)AppSettingsManager.GetParameterFiles("TestMode");
        }

        private string ChoseTypeOperationRecursive(OperationEntity operation, int depth, int maxDepth,TypeOperation typeOperation , List<ProductEntity> products = null)
        {
            try
            {
                if (depth >= maxDepth)
                {
                    throw new Exception("Неможливо виконати операцію");
                }

                string result = string.Empty;
                switch(typeOperation)
                {
                    case TypeOperation.OpenShift:
                        {

                            SendCheck(operation, TypeOperation.OpenShift);
                            result = "OK";
                            break;
                        }
                    case TypeOperation.CloseShift:
                        {
                            SendCheck(operation, TypeOperation.CloseShift);
                            result = "OK";
                            break;
                        }
                    case TypeOperation.DepositAndWithdrawalMoney:
                        {
                            SendCheck(operation, TypeOperation.DepositAndWithdrawalMoney);
                            result = "OK";
                            break;
                        }
                    case TypeOperation.FiscalCheck:
                        {
                            result = SendCheck(operation, TypeOperation.FiscalCheck, products);
                            break;
                        }
                    case TypeOperation.ReturnCheck:
                        {
                            result = SendCheck(operation, TypeOperation.ReturnCheck, products);
                            break;
                        }
                }
                return result;
            }
            catch (ExceptionBadHashPrev exbadhash)
            {
                operation.MAC = exbadhash.Error.Split(" ")[3];
                switch (typeOperation)
                {
                    case TypeOperation.OpenShift:
                        {

                            return ChoseTypeOperationRecursive(operation, depth + 1, maxDepth, TypeOperation.OpenShift);
                            break;
                        }
                    case TypeOperation.CloseShift:
                        {
                            return ChoseTypeOperationRecursive(operation, depth + 1, maxDepth, TypeOperation.CloseShift);
                            break;
                        }
                    case TypeOperation.DepositAndWithdrawalMoney:
                        {
                            return ChoseTypeOperationRecursive(operation, depth + 1, maxDepth, TypeOperation.DepositAndWithdrawalMoney);
                            break;
                        }
                    case TypeOperation.FiscalCheck:
                        {
                            return ChoseTypeOperationRecursive(operation, depth + 1, maxDepth, TypeOperation.FiscalCheck, products);
                            break;
                        }
                    case TypeOperation.ReturnCheck:
                        {
                            return ChoseTypeOperationRecursive(operation, depth + 1, maxDepth, TypeOperation.ReturnCheck, products);
                            break;   
                        }
                }
                return string.Empty;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return string.Empty;
            }
        }
        public string OpenShift(OperationEntity operation )
        {
            return ChoseTypeOperationRecursive(operation, 0, 5, TypeOperation.OpenShift);
        }

        public string CloseShift(OperationEntity operation)
        {
            return ChoseTypeOperationRecursive(operation, 0, 5, TypeOperation.CloseShift);

        }

        public string DepositAndWithdrawalMoney(OperationEntity operation)
        {
            return ChoseTypeOperationRecursive(operation, 0, 5, TypeOperation.DepositAndWithdrawalMoney);

        }

        public string SendFiscalCheck(OperationEntity operation,List<ProductEntity> products)
        {
            return ChoseTypeOperationRecursive(operation, 0, 5, TypeOperation.DepositAndWithdrawalMoney, products);

        }
        public string SendReturnFiscalCheck(OperationEntity operation, List<ProductEntity> products)
        {
            return ChoseTypeOperationRecursive(operation, 0, 5, TypeOperation.ReturnCheck, products);

        }

        private string SendCheck(OperationEntity operation , TypeOperation typeOperation , List<ProductEntity> products = null)
        {
            //WriteReadXmlFile.WriteXmlFile(operation, new List<OrderEntity>(), products, pathxml);
            //if (_signFileContoller.SignFile(Session.User.KeyPath, Session.User.KeyPassword))
            //{
            //    string result = string.Empty;
            //    switch (typeOperation)
            //    {
            //        case TypeOperation.OpenShift:
            //            {
            //                result = _fiscalServerController.SendServiceCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
            //                    Convert.ToInt32(operation.NumberPayment),  operation.FiscalNumberRRO, _testMode);
            //                break;
            //            }
            //            case TypeOperation.CloseShift:
            //            {
            //                result = _fiscalServerController.SendServiceCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
            //                    Convert.ToInt32(operation.NumberPayment), operation.FiscalNumberRRO, _testMode);
            //                break;
            //            }
            //        case TypeOperation.DepositAndWithdrawalMoney:
            //            {
            //                result = _fiscalServerController.SendServiceCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
            //                    Convert.ToInt32(operation.NumberPayment), operation.FiscalNumberRRO , _testMode);
            //                break;
            //            }
            //        case TypeOperation.FiscalCheck:
            //            {
            //                result = _fiscalServerController.SendFiscalCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
            //                    Convert.ToInt32(operation.NumberPayment), operation.FiscalNumberRRO , _testMode);
            //                break;
            //            }
            //        case TypeOperation.ReturnCheck:
            //            {
            //                result = _fiscalServerController.SendFiscalCheck(long.Parse(operation.CreatedAt.ToString("yyyyMMddHHmmss")),
            //                     Convert.ToInt32(operation.NumberPayment), operation.FiscalNumberRRO, _testMode);
            //                break;
            //            }
            //    }
            //    return result;
            //}
            //else
            //{
            //    throw new Exception("невдалося підписати файл");
            //}

            return string.Empty;
        }

        public string? GetMac()
        {
            try
            {
                OperationEntity operation = new OperationEntity();
                List<OrderEntity> orders = new List<OrderEntity>();
                Task t = Task.Run(async () =>
                {
                    operation = (await MainWebServerController.MainDataBaseConntroller.OperationController.GetLastOperation(Session.Token));
                    orders = (await MainWebServerController.MainDataBaseConntroller.OrderController.GetOrders(Session.Token)).ToList();
                });

                t.Wait();
                if (operation != null)
                {
                    List<OrderEntity> OperationOrders = orders.Where(item => item.Operation.ID == operation.ID).ToList();


                    List<ProductEntity> goodsList = new List<ProductEntity>();
                    //WriteReadXmlFile.WriteXmlFile(operation, OperationOrders, new List<ProductEntity>(), pathxml);
                    return SHA.GenerateSHA256File(pathxml);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
