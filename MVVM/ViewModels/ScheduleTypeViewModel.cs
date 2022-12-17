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
using System.Windows.Data;
using PIS8_2.MVVM.Model.Data;
using System.Windows;
using System.Windows.Controls;

namespace PIS8_2.MVVM.ViewModels
{
    internal class ScheduleTypeViewModel:ViewModel
    {
        private readonly Connection _conn;

        private Card _card;

        public Card Card
        {
            get => _card;
            set => SetField(ref _card, value, nameof(Card));
        }

        private List<string> _municips;
        public List<string> Municips
        {
            get => _municips;
            set => SetField(ref _municips, value, "Municips");
        }

        private List<string> _omsus;
        public List<string> Omsus
        {
            get => _omsus;
            set => SetField(ref _omsus, value, "Omsus");
        }

        private bool _isEditMode = false;

        public bool IsEditMode
        {
            get => _isEditMode;
            set => SetField(ref _isEditMode, value, "IsEditMode");
        }

        private bool _isReadOnly = true;

        public bool IsReadOnly
        {
            get => _isReadOnly;
            set => SetField(ref _isReadOnly, value, "IsReadOnly");
        }

        private List<DateTime> _blackOutTrappingDates;

        public List<DateTime> BlackOutTrappingDates
        {
            get => _blackOutTrappingDates;
            set => SetField(ref _blackOutTrappingDates, value, "BlackOutTrappingDates");
        }

        private CalendarDateRange _dateRange=new CalendarDateRange();

        private Visibility _moreBoxesVisibility;
        public Visibility MoreBoxesVisibility
        {
            get => _moreBoxesVisibility;
            set => SetField(ref _moreBoxesVisibility, value, nameof(MoreBoxesVisibility));
        }

        private Visibility _boxesMenuItemsVisibility;
        public Visibility BoxesMenuItemsVisibility
        {
            get => _boxesMenuItemsVisibility;
            set => SetField(ref _boxesMenuItemsVisibility, value, nameof(BoxesMenuItemsVisibility));
        }

        public ICommand BackToReestrCommand { get; }
        public ICommand ExportWordCommand { get; }
        public ICommand EditModeChangeCommand { get; }
        public ICommand SaveModeShangeCommand { get; }
        public ICommand AddModeChangeCommand { get; }

        public ScheduleTypeViewModel(NavigationStore navigationStore,UserStore userStore, Card selectedCard = null, ICommand openScheduleCardCommand = null)
        {
            if (selectedCard == null)
            {
                ChangeEditMode();
                MoreBoxesVisibility = Visibility.Visible;
                BoxesMenuItemsVisibility = Visibility.Collapsed;
                _card = new Card() {
                    IdMunicipNavigation = new Municip(),
                    IdOmsuNavigation = new Omsu(),
                    IdOrgNavigation = new Organisation(),
                    Datetrapping = DateTime.Now.AddDays(-3),
                    Datemk = DateTime.Now,
                    Dateworkorder = DateTime.Now.AddDays(-2),
                };
            }
            else
            {
                MoreBoxesVisibility = Visibility.Collapsed;
                BoxesMenuItemsVisibility = Visibility.Visible;
                _card = selectedCard;
            }

            BackToReestrCommand =
                new BackToReestrCommand(new NavigationService<ReestrViewModel>(navigationStore,
                    () => new ReestrViewModel(userStore, navigationStore)));

            ExportWordCommand = new ExportWordCommand(this);
            EditModeChangeCommand = new EditModeChangeCommand(this);

            SaveModeShangeCommand = new SaveModeChangeCommand(this);

            AddModeChangeCommand = new AddModeChangeCommand(_card, userStore, openScheduleCardCommand);

            _conn = new Connection();
            Municips = _conn.GetNamesMunicip();
            Omsus = _conn.GetNamesOMSU();
        }

        public void ChangeEditMode()
        {
            IsEditMode = true;
            IsReadOnly = false;
        }

        public void ChangeSaveMode()
        {
            IsEditMode = false;
            IsReadOnly = true;
        }
    }
}
