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
        public double rest { get; set; }
        public double sale { get; set; }
        public DateTimeOffset? created_at { get; set; }
        public User? user { get; set; }

        private ICollection<ProductOrder> productOrders { get; set; }

        public Order()
        {
            this.productOrders = new List<ProductOrder>();
        }
    }
}
