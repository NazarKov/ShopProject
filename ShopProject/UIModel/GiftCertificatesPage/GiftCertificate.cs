using ShopProject.UIModel.UserPage;
using ShopProjectDataBase.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.GiftCertificatesPage
{
    public class GiftCertificate
    {
        public int ID { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; } 
        public DateTime? CreateAt { get; set; }
        public TypeStatusGiftCertificate Status { get; set; }
        public DateTime EndAt { get; set; }
    }
}
