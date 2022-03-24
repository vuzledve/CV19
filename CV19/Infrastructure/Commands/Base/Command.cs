using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CV19.Infrastructure.Commands.Base
{
    internal class Command : ICommand
    {
        //активируется когда CanExecute начинает возвращать другое значение
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value; 
            remove => CommandManager.RequerySuggested -= value;
        }

        //может ли команда быть выполнена 
        public abstract bool CanExecute(object? parameter)
       

        //то, что должно быть выполнено командой (логика команды)
        public abstract void Execute(object? parameter)
        
    }
}
