using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Model
{
    public class CashRegister
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public double begin_day { get; set; }
        public double end_day { get; set; }
        public double sum { get; set; } 
        public double took_sum { get; set; }
        public DateTimeOffset? created_begin_day { get; set; }
        public DateTimeOffset? created_end_day { get; set; }

    }
}
