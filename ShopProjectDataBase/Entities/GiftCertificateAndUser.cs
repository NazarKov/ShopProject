using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProjectDataBase.Entities
{
    public class GiftCertificateAndUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; } 
        public DateTime IssuedAt { get; set; } 
        public DateTime EndAt { get; set; } 
        public GiftCertificatesEntity? GiftCertificates { get; set; }
        public UserEntity? User { get; set; }
    }
}
