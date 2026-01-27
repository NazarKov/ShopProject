using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopProject.Helpers.Command
{
    public class DelegateCommandAsync :ICommand
    {
        private readonly Func<Task> _execute;
        private bool _isExecuting;


        public DelegateCommandAsync(Func<Task> execute)
        {
            _execute = execute;
        }


        public bool CanExecute(object parameter) => !_isExecuting;


        public async void Execute(object parameter)
        {
            if (_isExecuting) return;


            _isExecuting = true;
            RaiseCanExecuteChanged();


            try
            {
                await _execute();
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }


        public event EventHandler CanExecuteChanged;
        private void RaiseCanExecuteChanged()
        => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
