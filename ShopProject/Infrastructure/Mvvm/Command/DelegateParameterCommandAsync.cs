using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopProject.Core.Mvvm.Command
{
    internal class DelegateParameterCommandAsync<T> : ICommand
    {
        private readonly Func<T, Task> _execute;
        private readonly Predicate<T> _canExecute;
        private bool _isExecuting;

        public DelegateParameterCommandAsync(Func<T, Task> execute)
            : this(execute, null) { }

        public DelegateParameterCommandAsync(Func<T, Task> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_isExecuting)
                return false;

            if (parameter == null && typeof(T).IsValueType)
                return _canExecute == null;

            return _canExecute == null || _canExecute((T)parameter);
        }

        public async void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;

            try
            {
                _isExecuting = true;
                RaiseCanExecuteChanged();

                await _execute((T)parameter);
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
