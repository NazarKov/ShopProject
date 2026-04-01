using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopProject.Core.Mvvm.CompositionRoot
{
    public class FactoryView
    {
        private readonly Dictionary<Type, object> _views = new();
        private readonly Dictionary<Type, Func<object>> _creators = new();

        public void Register<TView>(Func<TView> creator)
            where TView : FrameworkElement
        {
            _creators[typeof(TView)] = () => creator();
        }

        public TView Get<TView>() where TView : FrameworkElement
        {
            var type = typeof(TView);

            if (!_views.TryGetValue(type, out var instance))
            {
                if (!_creators.TryGetValue(type, out var creator))
                    throw new System.Exception($"View {type.Name} не зареєстрована");

                instance = creator();
                _views[type] = instance;
            }

            return (TView)instance;
        }
        public TView GetNew<TView>() where TView : FrameworkElement
        {
            var type = typeof(TView); 
            if (!_creators.TryGetValue(type, out var creator))
                throw new System.Exception($"View {type.Name} не зареєстрована");

            return (TView)creator();
        }

    }
}
