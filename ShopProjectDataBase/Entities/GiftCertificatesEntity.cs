using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProjectDataBase.Entities
{
    public class GiftCertificatesEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? code { get; set; }
        public DateTime? create_at { get; set; }
        public string? description { get; set; }
    }
}
