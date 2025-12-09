using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProjectDataBase.Entities
{
    public class GiftCertificatesEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string? Code { get; set; }
        public DateTime? CreateAt { get; set; }
        public string? Description { get; set; }
        public UserEntity? User { get; set; }

    }
}
