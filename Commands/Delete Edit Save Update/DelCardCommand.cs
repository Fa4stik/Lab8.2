using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Stores;
using System.Collections.Generic;
using System.Linq;

namespace PIS8_2.Commands
{
    internal class DelCardCommand : Command
    {
        private readonly RegistryViewModel _viewModel;
        private readonly Connection _conn;
        private readonly UserStore _user;

        public DelCardCommand(RegistryViewModel viewModel, UserStore user)
        {
            _conn = new Connection();
            _user = user;
            _viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var data = parameter as List<LimitedCard>;

            int[] idCards = data.Where(c => c.IsSelectedCard)
                .Select(c => c.Id)
                .ToArray();
            _conn.DeleteCards(idCards, _user.CurrentUser);
            _viewModel.UpdateRegistry.Execute(null);
        }
    }
}
