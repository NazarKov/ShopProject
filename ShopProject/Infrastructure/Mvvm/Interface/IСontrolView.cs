using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Core.Mvvm.Interface
{
    internal interface IСontrolView
    {
        public Action? CloseView { get; set; }
    }
}
