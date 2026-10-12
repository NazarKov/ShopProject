using System; 
using System.Windows.Input;

namespace ShopProject.Infrastructure.Mvvm.Command
{
    internal class DelegateParameterCommand<T> : ICommand
    {
        private readonly Action<T> _execute; 
        private readonly Predicate<T> _canExecute;

        public DelegateParameterCommand(Action<T> execute) : this(execute, canExecute: null) { }

        public DelegateParameterCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null) throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            if (parameter == null)
            {
                // якщо T — value type (int, bool...), не можна кастити null
                if (typeof(T).IsValueType)
                    return false;

                return _canExecute(default);
            }

            return _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            if (parameter == null)
            {
                if (typeof(T).IsValueType)
                    throw new InvalidOperationException("Parameter cannot be null for value type.");

                _execute(default);
                return;
            }

            _execute((T)parameter);
        }

        public void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
