using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.NetworkServise.ElectronicTaxAccountPublicApi.Model
{
    public class Values
    {
        public string TIN { get; set; }
        public string FULL_NAME { get; set; }
        public int? C_STI_MAIN { get; set; }
        public string C_STI_MAIN_NAME { get; set; }
        public string D_REG_STI { get; set; }
        public string N_REG_STI { get; set; }
        public object IS_OZN_VEZ { get; set; }
        public object D_ZAKR_STI { get; set; }
        public string ADR_NS { get; set; }
        public string TELEPHON { get; set; }
        public string KVED { get; set; }
        public string KVED_NAME { get; set; }
        public string TERMINATION { get; set; }
        public string DATA_N { get; set; }
        public int? GRUP { get; set; }
        public int? STAVKA { get; set; }
        public object DAT_ANUL { get; set; }
        public string DATE_ACC_ERS { get; set; }
        public string ID_ERS { get; set; }
        public object DATE_DCC_ERS { get; set; }
    }
}
