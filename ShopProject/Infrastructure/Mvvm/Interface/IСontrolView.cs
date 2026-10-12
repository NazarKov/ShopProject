using System; 

namespace ShopProject.Infrastructure.Mvvm.Interface
{
    internal interface IСontrolView
    {
        public Action? CloseView { get; set; }
    }
}
