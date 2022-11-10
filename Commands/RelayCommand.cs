using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIS8_2.Commands.Base;

namespace PIS8_2.Commands
{
    internal class RelayCommand:Command
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> Execute,Func<object,bool> CanExecute=null)
        {
            _execute = Execute?? throw new ArgumentException(nameof(Execute));
            _canExecute = CanExecute;
        }
        public override bool CanExecute(object parameter)=>_canExecute?.Invoke(parameter)??true;
        

        public override void Execute(object parameter) => _execute(parameter);
        
    }
}
