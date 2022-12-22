using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.ExportExcel;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Stores;
using System.Linq;

namespace PIS8_2.Commands.Export
{
    internal class ExportExcelCommand : Command
    {
        private readonly RegistryViewModel _registryViewModel;
        private readonly UserStore _userStore;

        public ExportExcelCommand(RegistryViewModel registryViewModel, UserStore userStore)
        {
            _registryViewModel = registryViewModel;
            _userStore = userStore;
        }

        public override bool CanExecute(object parameter) => _registryViewModel.Cards.Count != 0;

        public override void Execute(object parameter)
        {
            var reportExcel = new ExportExcelReestr().GenerateReport(_userStore.CurrentUser, _registryViewModel.Filter, _registryViewModel.SortingList.ToList());
            new ExportExcelReestr().SaveToExcel(reportExcel);
        }
    }
}
