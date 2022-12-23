using PIS8_2.Commands.Base;
using PIS8_2.MVVM.ViewModels;

namespace PIS8_2.Commands
{
    internal class MovePageCommand:Command
    {
        private readonly RegistryViewModel _viewModel;

        public MovePageCommand(RegistryViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return parameter.ToString() switch
            {
                "+" => _viewModel.CurrentPage + 1 <= _viewModel.MaxPage,
                "-" => _viewModel.CurrentPage - 1 != 0,
                "min" => _viewModel.CurrentPage != 1,
                "max" => _viewModel.CurrentPage != _viewModel.MaxPage,
                _ => false
            };
        }

        public override void Execute(object parameter)
        {
            switch (parameter.ToString())
            {
                case "+":
                    _viewModel.CurrentPage++;
                    break;
                case "-":
                    _viewModel.CurrentPage--;
                    break;
                case "min":
                    _viewModel.CurrentPage=1;
                    break;
                case "max":
                    _viewModel.CurrentPage=_viewModel.MaxPage;
                    break;
            }

            _viewModel.IsAllCheckedItems = false;
            _viewModel.UpdateRegistry.Execute(null);
        }
    }
}
