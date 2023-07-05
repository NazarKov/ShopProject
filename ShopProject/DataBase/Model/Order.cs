using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShopProject.DataBase.Model
{
    internal class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        public double suma { get; set; }
        public double userSuma { get; set; }
        //сума яку дала людина добавити
        public double rest { get; set; }
        public double sale { get; set; }
        public string status { get; set; }
        public string LocalNumber { get; set; }
        //тип оплати
        public string type_oplat { get; set; }
        public DateTimeOffset? created_at { get; set; }
        public User? user { get; set; }

        public ICollection<ProductOrder> OrderItem { get; set; }

    }
}
