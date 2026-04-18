using ShopProject.Core.Mvvm;
using ShopProject.Extensions.FactoryExtensions;
using ShopProject.Infrastructure.CompositionRoot.Interface;
using ShopProject.ViewModel;
using ShopProject.ViewModel.HomePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Infrastructure.CompositionRoot
{
    public class AppCompositionRoot
    {
        public ServiceProvider ServiceProvider { get; private set; } = new();
        public FactoryView FactoryView { get; private set; } = new();

        public TView GetViewWithViewModel<TView, TViewModel>() where TView : FrameworkElement where TViewModel : ViewModel<TViewModel>
        {
            var view = FactoryView.Get<TView>();
            view.DataContext = ServiceProvider.Get<TViewModel>();

            ViewModelLoadResourse(view);
            return view;
        }
        public TView GetNewViewWithViewModel<TView, TViewModel>() where TView : FrameworkElement where TViewModel : ViewModel<TViewModel>
        {
            var view = FactoryView.GetNew<TView>();
            view.DataContext = ServiceProvider.Get<TViewModel>();

            ViewModelLoadResourse(view);

            return view;
        }

        public T GetService<T>()
        {
            return ServiceProvider.Get<T>();
        }

        private static void ViewModelLoadResourse<TView>(TView viewModel) where TView : FrameworkElement
        {
            if (viewModel.DataContext is IViewModelLoadResourse vm)
            {
                vm.LoadResourse();
            }
        }
    }
}
