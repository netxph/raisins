using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Raisins.Client.Raffle
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public RelayCommand(
            Predicate<object> canExecute,
            Action<object> execute)
        {
            _canExecute = canExecute;

            if(execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
        }
        
        public bool CanExecute(object parameter)
        {
            return _canExecute != null ? _canExecute(parameter) : true; 
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
