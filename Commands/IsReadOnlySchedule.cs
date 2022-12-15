using PIS8_2.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PIS8_2.Commands
{
    internal class IsReadOnlySchedule : Command
    {
        private bool _isEditMode;
        public IsReadOnlySchedule(bool isEditMode)
        {
            _isEditMode = isEditMode;
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => _isEditMode = false;
    }
}
