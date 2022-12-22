using PIS8_2.Commands.Base;
using PIS8_2.MVVM.ViewModels;

namespace PIS8_2.Commands
{
    class ResetFilterCommand:Command
    {
        private readonly RegistryViewModel _viewModel;

        public ResetFilterCommand(RegistryViewModel viewModel)
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
