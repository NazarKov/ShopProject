using ShopProject.Model.Enum;
using ShopProject.Model.UI.Discount;
using ShopProject.Model.UI.MediaAccessControl;
using ShopProject.Model.UI.Order; 
using ShopProject.Model.UI.WorkingShift;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq; 

namespace ShopProject.Model.UI.Operation
{
    public class OperationModel
    {
        public int ID { get; set; }
        public string FiscalServerId { get; set; } = string.Empty;
        public TypePayment TypePayment { get; set; }
        public string TypePaymentString
        {
            get { return OperationTypePaymentStatusModel.GetStatus().ElementAt(System.Enum.GetValues<TypePayment>().ToList().IndexOf(TypePayment)); }
            set { TypePayment = System.Enum.GetValues<TypePayment>().ToList().ElementAt(OperationTypePaymentStatusModel.GetStatus().IndexOf(value)); }
        }
        public TypeOperation TypeOperation { get; set; }
        public decimal BuyersAmount { get; set; } = decimal.Zero;
        public decimal RestPayment { get; set; } = decimal.Zero;
        public decimal TotalPayment { get; set; } = decimal.Zero;
        public string NumberPayment { get; set; } = string.Empty;
        public string GoodsTax { get; set; } = string.Empty;
        [Required]
        public MediaAccessControlModel? MAC { get; set; }
        public DateTime CreatedAt { get; set; }
        public DiscountModel? Discount { get; set; }
        public WorkingShiftModel? Shift { get; set; }
        public ICollection<OrderModel>? Order { get; set; }
    }
}
