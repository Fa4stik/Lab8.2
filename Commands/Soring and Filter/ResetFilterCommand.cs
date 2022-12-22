using PIS8_2.Commands.Base;
using PIS8_2.MVVM.ViewModels;

namespace PIS8_2.Commands
{
    class ResetFilterCommand:Command
    {
        private readonly ReestrViewModel _viewModel;

        public ResetFilterCommand(ReestrViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object parameter) => true;


        public override void Execute(object parameter)
        {
            _viewModel.Filter.StartLocality="asdasdasd";
            _viewModel.Filter.StartTypeOrder = "asdasdasd";


        }
    }
}
