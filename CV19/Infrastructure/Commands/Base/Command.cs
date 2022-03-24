using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CV19.Infrastructure.Commands.Base
{
    internal abstract class Command : ICommand
    {
        //генерируется когда CanExecute начинает возвращать другое значение
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        //может ли быть выполнена команда
        public abstract bool CanExecute(object parameter);

        //логика команды (то, что должно быть выполнено командой)
        public abstract void Execute(object parameter);
    }
}
