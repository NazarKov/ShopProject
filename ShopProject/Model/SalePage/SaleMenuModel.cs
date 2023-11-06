using FiscalServerApi;
using FiscalServerApi.ExceptionServer;
using ShopProject.DataBase.DataAccess.EntityAccess;
using ShopProject.DataBase.Interfaces;
using ShopProject.DataBase.Model;
using ShopProject.Helpers;
using ShopProject.Helpers.HelperForPrinting;
using ShopProject.Helpers.MiniServiceSigningFile;
using ShopProject.Model.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using static NPOI.HSSF.Util.HSSFColor;

namespace ShopProject.Model.SalePage
{
    internal class SaleMenuModel
    {
        private List<Goods> _goods;
        private IEntityAccessor<Goods> _goodsRepository;
        private IEntityAccessor<Operation> _operationRepository;
        private IEntityAccessor<GoodsOperation> _goodsOperationRepository;

        private MainContoller _mainContoller;
        private FiscalServerController _serverController;
        private PrintingFiscalCheck _printingFiscalCheck;
        private bool _isDrawingChek;

        public bool IsDrawinfChek { get { return _isDrawingChek; } set { _isDrawingChek = value; } }


        string pathxml = "..\\..\\..\\Resource\\BufferStorage\\Chek.xml";

        public SaleMenuModel()
        {
            _goods = new List<Goods>();
            _goodsRepository = new GoodsTableAccess();
            _operationRepository = new OperationTableAccess();
            _goodsOperationRepository = new GoodsOperationTableAccess();

            _mainContoller = new MainContoller();
            _serverController = new FiscalServerController();
            _printingFiscalCheck = new PrintingFiscalCheck();
            _isDrawingChek = true;
        }
        
        public Goods Search(string barCode)
        {
            try
            {
                return _goodsRepository.GetItemBarCode(barCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public string GetMac(bool typechek)
        {
            try
            {
                Operation operation = GetLastChek();

                List<GoodsOperation> goodsOperation = _goodsOperationRepository.GetAll().Where(item => item.operation.id == operation.id).ToList();
                List<Goods> goodsList = new List<Goods>();
                RecordingOperationXmlFile.WriteXmlFile(operation, goodsOperation, typechek,pathxml);
                return SHA.GenerateSHA256File(pathxml);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
        
        public string GetLocalNumber()
        {
            try
            {
                Operation operation = GetLastChek();
                return (Convert.ToInt32(operation.numberPayment)+1).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
                return null;
            }
        }
        
        private void SaveDataBase(Operation operation, List<Goods> goods)
        {
            _operationRepository.Add(operation);
            if (goods.Count != 0)
            {
                foreach (Goods item in goods)
                {
                    _goodsOperationRepository.Add(new GoodsOperation()
                    {
                        operation = operation,
                        goods = item,
                        count = (int)item.count,
                    });
                }
            }

        }
        
        public bool SendChek(List<Goods> goods,Operation operation)
        {
            try
            {
                //ChekedOpenChange();
                SendChek(operation, goods);
                return true;
            }
            catch (ExceptionBadHashPrev exbadHas)
            {
                operation.mac = exbadHas.Mac;
                SendChek(operation, goods);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
        }      
        
        private void SendChek(Operation operation,List<Goods> goods)
        {
            if (operation.typeOperation == 0)
            {
                RecordingOperationXmlFile.WriteXmlFile(operation, goods, true, pathxml);


                _mainContoller.SignFiles();


                var id =  _serverController.SendFiscalCheck(long.Parse(operation.createdAt.ToString("yyyyMMddHHmmss")), Convert.ToInt32(operation.numberPayment), operation.fiscalNumberRRO , true);
                
                if (id != null)
                {
                    SaveDataBase(operation, goods);
                    if (IsDrawinfChek)
                    {
                        PrintChek(goods, operation, id);
                    }
                }
            }
            else
            {
                SaveDataBase(operation, goods);
            }


        }
        public void PrintChek(List<Goods> products, Operation order,string id)
        {
            _printingFiscalCheck.PrintCheck(products,id,order);
        }

        
        private Operation GetLastChek()
        {
            var operationList = _operationRepository.GetAll().Where(item => item.typeOperation == 0);
            if(operationList!=null)
            {
                return operationList.ElementAt(operationList.Count() - 1);
            }
            else
            {
                throw new Exception("Зачекайте");
            }
        }

       

    }
}
