using System;
using System.Windows.Input;

namespace Image_Gallery.ViewModel
{
    public class DelegateCommand : ICommand
    {
        Action<object> _execute;
        Predicate<object> _canExecute;

        #region Constructors
        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {

        }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }
        #endregion

        #region Methods
        public bool CanExecute(object parameter)
        {
            return _canExecute != null ? _canExecute(parameter) : true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion
    }
}