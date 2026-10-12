using ShopProject.Infrastructure.Mvvm;
using ShopProject.Infrastructure.Mvvm.Interface; 
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
