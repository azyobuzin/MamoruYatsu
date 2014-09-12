using System;
using System.Windows.Input;

namespace MamoruYatsu
{
    class DelegateCommand : ICommand
    {
        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute ?? (() => true);
        }

        public DelegateCommand(Action execute) : this(execute, null) { }

        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public bool CanExecute(object parameter)
        {
            return this.canExecute();
        }

        public void Execute(object parameter)
        {
            this.execute();
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
                this.CanExecuteChanged(this, EventArgs.Empty);
        }
    }

    class DelegateCommand<T> : ICommand
    {
        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute ?? (_ => true);
        }

        public DelegateCommand(Action<T> execute) : this(execute, null) { }

        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        public bool CanExecute(object parameter)
        {
            return this.canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            this.execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
                this.CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
