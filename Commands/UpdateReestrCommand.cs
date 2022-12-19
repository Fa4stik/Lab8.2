using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIS8_2.Commands.Base;
using PIS8_2.Converters;
using PIS8_2.MVVM.Model;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;
using PIS8_2.Stores;

namespace PIS8_2.Commands
{
    internal class UpdateReestrCommand:Command
    {
        private readonly ReestrViewModel _viewModel;
        private readonly UserStore _userStore;
        private readonly Connection _conn;

        public UpdateReestrCommand(ReestrViewModel viewModel, UserStore userStore)
        {
            _viewModel = viewModel;
            _userStore = userStore;
            _conn = new Connection();
        }

        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter)
        {
            IEnumerable<Card> cards;
            if (_viewModel.Filter==null)
            {
                cards = _conn.ExecuteCardsWithFilter(_userStore.CurrentUser,null, _viewModel.SortingList.ToList());
            }
            else
            {
                cards = _conn.ExecuteCardsWithFilter(_userStore.CurrentUser,_viewModel.Filter,_viewModel.SortingList.ToList());
            }
            _viewModel.Cards = ConverterCardsToLimitedCards.ConvertCardsToLimitedCards(cards)
                .ToList();
            _viewModel.MaxPage = (int)Math.Ceiling((double)_viewModel.Cards.Count/100);
            _viewModel.Cards=_viewModel.Cards
                .Skip((_viewModel.CurrentPage - 1) * 100)
                .Take(_viewModel.CurrentPage * 100)
                .ToList();

        }
    }
}
