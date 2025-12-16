using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShopProjectDataBase.Helper;

namespace ShopProjectDataBase.Entities
{
    public class GiftCertificatesEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public TypeStatusGiftCertificate Status {  get; set; } 
        public DateTime? CreateAt { get; set; }  

    }
}
