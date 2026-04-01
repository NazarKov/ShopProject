using ShopProject.Model.Domain.Discount;
using ShopProject.Model.Domain.MediaAccessControl;
using ShopProject.Model.Domain.Order;
using ShopProject.Model.Domain.WorkingShift;
using ShopProject.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.Domain.Operation
{
    public class Operation
    {
        public int ID { get; set; }
        public string FiscalServerId { get; set; } = string.Empty; 
        public TypePayment TypePayment { get; set; } 
        public TypeOperation TypeOperation { get; set; } 
        public decimal BuyersAmount { get; set; } = decimal.Zero; 
        public decimal RestPayment { get; set; } = decimal.Zero; 
        public decimal TotalPayment { get; set; } = decimal.Zero; 
        public string NumberPayment { get; set; } = string.Empty; 
        public string GoodsTax { get; set; } = string.Empty; 
        [Required]
        public MediaAccessControl.MediaAccessControl? MAC { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public Discount.Discount? Discount { get; set; } 
        public WorkingShift.WorkingShift? Shift { get; set; } 
        public ICollection<Order.Order>? Order { get; set; }
    }
}
