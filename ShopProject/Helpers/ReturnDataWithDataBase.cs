using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Helpers
{
    internal class ReturnDataWithDataBase
    {
        private IEntityAccess<OperationEntiti> _operationRepository;
        private IEntityAccess<OrderEntiti> _goodsOperationRepository;
        List<OperationEntiti> list;


        public ReturnDataWithDataBase() 
        {
            _operationRepository = new OperationTableAccess();
            _goodsOperationRepository = new OrderTableAccess();
            list = new List<OperationEntiti>();
        }

        public string? GetMac(string pathxml)
        {
            try
            {
                OperationEntiti operation = GetLastCheck();
                if (operation != null)
                {
                    List<OrderEntiti> goodsOperation = _goodsOperationRepository.GetAll().Where(item => item.Operation.ID == operation.ID).ToList();
                    List<ProductEntiti> goodsList = new List<ProductEntiti>();
                    WriteReadXmlFile.WriteXmlFile(operation, goodsOperation, new List<ProductEntiti>(), pathxml);
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

        public string? GetLocalNumber()
        {
            try
            {
                OperationEntiti operation = GetLastCheck();
                if (operation == null)
                {
                    return "1";
                }
                else
                { 
                    return (Convert.ToInt32(operation.NumberPayment) + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }


        public OperationEntiti GetLastCheck()
        {

            list = (List<OperationEntiti>)_operationRepository.GetAll();
            if (list!=null)
            {
                var operationList = list.Where(item => item.TypeOperation != 200);
                if (operationList != null)
                {
                    return operationList.ElementAt(operationList.Count() - 1);
                }
                else
                {
                    throw new Exception("Зачекайте");
                }
            }
            else
            {
                return null;
            }
        }

        public decimal GetTotalNumberFiscalChechAndReturnFiscalCheck(decimal typeOperation)
        {
            list = (List<OperationEntiti>)_operationRepository.GetAll();
            if (list!=null)
            {
                //List <OperationEntiti> item = list.Where(item => item.TypeOperation == typeOperation).ToList().Where(item => item.CreatedAt.ToString("d")==DateTime.Now.ToString("d")).ToList();
                //if (item != null)
                //{
                //    return item.Count();
                //}
                //else
                //{
                    return decimal.Zero;
                //}
            }
            else
            {
                return decimal.Zero;
            }
        }

        public decimal GetTotalSumReceivedAndIssuance(decimal typeOperation)
        {
            list = (List<OperationEntiti>)_operationRepository.GetAll();
            if (list != null)
            {
                //var item = list.Where(item => item.TypeOperation == typeOperation).ToList().Where(item => item.CreatedAt.ToString("d") == DateTime.Now.ToString("d")).ToList();
                //if (item != null)
                //{
                //    decimal totalsum = 0;
                //    foreach(OperationEntiti operation in item)
                //    {
                //        totalsum += operation.TotalPayment;
                //    }
                //    return totalsum;
                //}
                //else
                //{
                   return decimal.Zero;
                //}
            }
            else
            {
                return decimal.Zero;
            }
        }
        public decimal GetTotalBuyersAmountAndRestOperation(string type,decimal typePayment)
        {
            list = (List<OperationEntiti>)_operationRepository.GetAll();
            if (list != null)
            {
                //var item = list.Where(item => item.TypeOperation == 0).ToList().Where(item => item.FormOfPayment == typePayment).ToList().Where(item => item.CreatedAt.ToString("d") == DateTime.Now.ToString("d")).ToList();
                //if (item != null)
                //{
                //    decimal totalsum = 0;
                //    switch (type)
                //    {
                //        case "buyersAmount":
                //            {
                //                foreach (OperationEntiti operation in item)
                //                {
                //                    totalsum += operation.BuyersAmount;
                //                }
                //                break;
                //            }
                //        case "restPayment":
                //            {
                //                foreach (OperationEntiti operation in item)
                //                {
                //                    totalsum += operation.RestPayment;
                //                }
                //                break;
                //            }
                //    }
                //    return totalsum;
                //}
                //else
                //{
                  return decimal.Zero;
                //}
            }
            else
            {
                return decimal.Zero;
            }
        }
        public decimal GetTotalRestReturnOperation(decimal typePayment)
        {
            list = (List<OperationEntiti>)_operationRepository.GetAll();
            if (list != null)
            {
                //var item = list.Where(item => item.TypeOperation == 1).ToList().Where(item => item.FormOfPayment == typePayment).ToList().Where(item => item.CreatedAt.ToString("d") == DateTime.Now.ToString("d")).ToList();
                //if (item != null)
                //{
                //    decimal totalsum = 0;
                //    foreach (OperationEntiti operation in item)
                //    {
                //        totalsum += operation.TotalPayment;
                //    }
                //    return totalsum;
                //}
                //else
                //{
                    return decimal.Zero;
                //}
            }
            else
            {
                return decimal.Zero;
            }
        }
    }
}
