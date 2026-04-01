using ShopProject.Model.Domain.PorductCodeUKTZED;
using ShopProject.Model.Domain.Product;
using ShopProject.Model.Domain.ProductUnit;
using ShopProject.Model.Domain.User;
using ShopProject.Model.Domain.UserRole;
using ShopProject.Model.Domain.WorkingShift;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ShopProject.Services.Modules.Session.Interface
{
    internal interface ISessionService
    {
        public User User { get; set; }
        public IEnumerable<ProductCodeUKTZED>? ProductCodesUKTZED { get; set; }
        public IEnumerable<ProductUnit>? ProductUnits { get; set; }
        public IEnumerable<UserRole>? Roles { get; set; }
        public WorkingShiftStatus WorkingShiftStatus { get; set; }
        public ObservableCollection<TabItem> Tabs { get; set; }


        public Product? UpdateProduct { get; set; }
        public IEnumerable<Product>? UpdateProductRange { get; set; }
        public ProductUnit? UpdateProductUnit { get; set; }
        public ProductCodeUKTZED? UpdateProductCodeUKTZED { get; set; }
        public bool CheckingSession();
        public bool CheckingWorkingShiftStatus();
        public bool RemoveSession();
    }
}
