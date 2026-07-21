using ShopProject.Core.Mvvm;
using ShopProject.Model.Domain.TaxObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Model.UI.TaxObject
{
    internal class TaxObjectSelectItemModel : Model<TaxObjectSelectItemModel>
    {
        public TaxObjectModel TaxObject { get; set; }
        private bool _isActive { get; set; } 
        public bool IsActive {
            get { return _isActive; }
            set { _isActive = value; OnPropertyChanged(nameof(IsActive)); }
        }
    }
}
