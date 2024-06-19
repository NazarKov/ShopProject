using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Helpers.HttpService.Model
{
    public class ListValue
    {
        public object FNUM { get; set; }
        public int LNUM { get; set; }
        public string NAME { get; set; }
        public string STATUS { get; set; }
        public string D_REG { get; set; }
        public string ADDRESS { get; set; }
        public string TYPE_OBJECT_NAME { get; set; }
        public int? TO_CODE { get; set; }
        public string STAN_OBJECT { get; set; }
        public string TYPE_OF_RIGHTS { get; set; }
        public string D_ACC_START { get; set; }
        public string D_ACC_END { get; set; }
        public string C_DISTR { get; set; }
        public string D_LAST_CH { get; set; }
        public int? C_TERRIT { get; set; }
        public object REG_NUM_OBJ { get; set; }
        public string KATOTTG { get; set; }
        public int? MFO { get; set; }
        public string MFO_NAME { get; set; }
        public string CLIENT_COUNT { get; set; }
        public int? TIN_BANK { get; set; }
        public string CODE_CURRENCY_NAME { get; set; }
        public string DATE_CREATE_COUNT { get; set; }
        public string DATE_REGIST { get; set; }
        public string KVED { get; set; }
        public string KVED_NAME { get; set; }
        public int? IS_MAIN { get; set; }
    }
}
