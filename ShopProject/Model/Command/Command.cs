﻿using System;
using System.Windows.Input;

namespace ShopProject.Model.Command
{
    internal abstract class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public virtual bool CanExecute()
        {
            return true;
        }

        public abstract void Execute();

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged(EventArgs.Empty);
        }

        bool ICommand.CanExecute(object? parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object? parameter)
        {
            Execute();
        }

    }
}
