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
        private IEntityAccessor<Operation> _operationRepository;
        private IEntityAccessor<GoodsOperation> _goodsOperationRepository;
        List<Operation> list;


        public ReturnDataWithDataBase() 
        {
            _operationRepository = new OperationTableAccess();
            _goodsOperationRepository = new GoodsOperationTableAccess();
            list = new List<Operation>();
        }

        public string? GetMac(string pathxml)
        {
            try
            {
                Operation operation = GetLastCheck();
                if (operation != null)
                {
                    List<GoodsOperation> goodsOperation = _goodsOperationRepository.GetAll().Where(item => item.operation.id == operation.id).ToList();
                    List<Goods> goodsList = new List<Goods>();
                    WriteReadXmlFile.WriteXmlFile(operation, goodsOperation, new List<Goods>(), pathxml);
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
                Operation operation = GetLastCheck();
                if (operation == null)
                {
                    return "1";
                }
                else
                { 
                    return (Convert.ToInt32(operation.numberPayment) + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }


        public Operation GetLastCheck()
        {

            list = (List<Operation>)_operationRepository.GetAll();
            if (list!=null)
            {
                var operationList = list.Where(item => item.typeOperation != 200);
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
            list = (List<Operation>)_operationRepository.GetAll();
            if (list!=null)
            {
                List <Operation> item = list.Where(item => item.typeOperation == typeOperation).ToList().Where(item => item.createdAt.ToString("d")==DateTime.Now.ToString("d")).ToList();
                if (item != null)
                {
                    return item.Count();
                }
                else
                {
                    return decimal.Zero;
                }
            }
            else
            {
                return decimal.Zero;
            }
        }

        public decimal GetTotalSumReceivedAndIssuance(decimal typeOperation)
        {
            list = (List<Operation>)_operationRepository.GetAll();
            if (list != null)
            {
                var item = list.Where(item => item.typeOperation == typeOperation).ToList().Where(item => item.createdAt.ToString("d") == DateTime.Now.ToString("d")).ToList();
                if (item != null)
                {
                    decimal totalsum = 0;
                    foreach(Operation operation in item)
                    {
                        totalsum += operation.totalPayment;
                    }
                    return totalsum;
                }
                else
                {
                    return decimal.Zero;
                }
            }
            else
            {
                return decimal.Zero;
            }
        }
        public decimal GetTotalBuyersAmountAndRestOperation(string type)
        {
            list = (List<Operation>)_operationRepository.GetAll();
            if (list != null)
            {
                var item = list.Where(item => item.typeOperation == 0).ToList().Where(item => item.createdAt.ToString("d") == DateTime.Now.ToString("d")).ToList();
                if (item != null)
                {
                    decimal totalsum = 0;
                    switch (type)
                    {
                        case "buyersAmount":
                            {
                                foreach (Operation operation in item)
                                {
                                    totalsum += operation.buyersAmount;
                                }
                                break;
                            }
                        case "restPayment":
                            {
                                foreach (Operation operation in item)
                                {
                                    totalsum += operation.restPayment;
                                }
                                break;
                            }
                    }
                    return totalsum;
                }
                else
                {
                    return decimal.Zero;
                }
            }
            else
            {
                return decimal.Zero;
            }
        }
    }
}
