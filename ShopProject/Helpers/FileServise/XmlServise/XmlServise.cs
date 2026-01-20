using ShopProject.UIModel.SalePage;
using ShopProject.UIModel.StoragePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.FileServise.XmlServise
{
    public class XmlServise
    {
        private static readonly string _pathFile = "C:\\ProgramData\\ShopProject\\Temp\\Chek.xml";
        private XmlFile _xmlFile; 
        public XmlServise()
        {
            _xmlFile = new XmlFile();
        }

        public void CreateXMLFileOpenShift(WorkingShift workingShift  ) => _xmlFile.WriteOpenShift(_pathFile, workingShift );
        public void CreateXMLFileFiscalCheck(WorkingShift workingShift,  Operation operation, IEnumerable<Product> products) =>
            _xmlFile.WriteFiscalCheck(_pathFile, workingShift,  operation, products);

        public void CreateXMLFileCloseShift(WorkingShift workingShift  )=> _xmlFile.WriteCloseShift(_pathFile,workingShift );

        public static string GenerationMACForXML()
        {
            return SHA.GenerateSHA256File(_pathFile);
        }

        public void CreateXMLFileWithdrawalMoney(WorkingShift workingShift , Operation operation) =>_xmlFile.WriteWithdrawalMoney(_pathFile, workingShift , operation);
        public void CreateXMLFileDepositMoney(WorkingShift workingShift, Operation operation) => _xmlFile.WriteDepositMoney(_pathFile, workingShift, operation);
    }
}
