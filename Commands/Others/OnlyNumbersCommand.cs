using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using PIS8_2.Commands.Base;

namespace PIS8_2.Commands.Others
{
    internal class OnlyNumbersCommand:Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            var eventArgs = parameter as TextCompositionEventArgs;
            Regex regex = new Regex("[^0-9]+");
            eventArgs.Handled = regex.IsMatch(eventArgs.Text);
        }
    }
}
