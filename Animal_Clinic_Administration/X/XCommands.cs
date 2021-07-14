using Animal_Clinic_Administration.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Animal_Clinic_Administration.X
{
    class XCommand : ICommand
    {
        public XCommand(Action<object> executeAction) : this(executeAction, null) { }
        public XCommand(Action<object> executeAction, Func<object,bool> canExecuteFunc)
        {
            execute = executeAction;
            canExecute = canExecuteFunc;
             
        }

        Action<object> execute;
        Func<object,bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {

            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
        
            if (execute != null) execute(parameter);
  
        }

    }
}
