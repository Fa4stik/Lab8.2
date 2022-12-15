using PIS8_2.Commands;
using PIS8_2.Service;
using PIS8_2.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PIS8_2.MVVM.Model;

namespace PIS8_2.MVVM.ViewModels
{
    internal class ScheduleTypeViewModel:ViewModel
    {
        private Card _card;

        public Card Card
        {
            get => _card;
            set => SetField(ref _card, value, nameof(Card));
        }

        private bool _isEditMode = false;

        public bool IsEditMode
        {
            get => _isEditMode;
            set => SetField(ref _isEditMode, value, "IsEditMode");
        }

        public bool IsReadOnly
        {
            get => !_isEditMode;
        }

        public ICommand BackToReestrCommand { get; }
        public ICommand ExportWordCommand { get; }
        public ScheduleTypeViewModel(NavigationStore navigationStore,UserStore userStore, Card selectedCard)
        {
            _card = selectedCard;

            BackToReestrCommand =
                new BackToReestrCommand(new NavigationService<ReestrViewModel>(navigationStore,
                    () => new ReestrViewModel(userStore, navigationStore)));

            ExportWordCommand = new ExportWordCommand(this);
        }
    }
}
