using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Model
{
    public class GiftCertificates
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? code { get; set; }
        public DateTime? create_at { get; set; }
        public string? description { get; set; }
    }
}
