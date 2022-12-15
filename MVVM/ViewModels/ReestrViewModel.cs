using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PIS8_2.Commands;
using PIS8_2.MVVM.Model;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.Service;
using PIS8_2.Stores;

namespace PIS8_2.MVVM.ViewModels
{
    internal class ReestrViewModel:ViewModel
    {
        private UserStore UserStore { get; }

        private FilterModel _filter;

        public FilterModel Filter
        {
            get => _filter;
            set => SetField(ref _filter, value, nameof(Filter));
        }
        public string Login => UserStore.CurrentUser.Login;



        private Visibility _filterVisability;

        public Visibility FilterVisability
        {
            get => _filterVisability;
            set => SetField(ref _filterVisability, value);
        }

        private List<LimitedCard> _cards;
        public List<LimitedCard> Cards
        {
            get => _cards;
            set => SetField(ref _cards, value, nameof(Cards));
        }

        public ICommand ExitCommand { get; }
        public ICommand AddRequestCommand { get; }
        public ICommand AddScheduleCommand { get; }
        public ICommand UpdateReestr { get; }
        public ICommand OpenScheduleCardCommand { get; }
        public ICommand ExportExcelCommand { get; }
        public ICommand ApplyFilter { get; }

        public ICommand ResetFilter { get; }



        public ReestrViewModel(UserStore userStore, NavigationStore navigationStore)
        {
            UserStore = userStore;

            FilterVisability = Visibility.Collapsed;



            UpdateReestr = new UpdateReestrCommand(this, userStore);
            UpdateReestr.Execute(null);

            OpenScheduleCardCommand = new OpenScheduleCardCommand(
                new ParameterNavigationService<Card, ScheduleTypeViewModel>(navigationStore,
                    (parameter) => new ScheduleTypeViewModel(navigationStore, userStore, parameter)));


            ExitCommand =
                new ExitCommand(new NavigationService<LoginViewModel>(navigationStore,
                    () => new LoginViewModel(navigationStore, userStore)));
            //AddScheduleCommand =
            //    new AddScheduleTypeCommand(new NavigationService<ScheduleTypeViewModel>(navigationStore,
            //        () => new ScheduleTypeViewModel(navigationStore)));
            AddRequestCommand =
                new AddRequestTypeCommand(new NavigationService<RequestTypeViewModel>(navigationStore,
                    () => new RequestTypeViewModel(navigationStore,userStore)));

            ExportExcelCommand = new ExportExcelCommand(this);

            ApplyFilter=new ApplyFilterCommand(this,userStore);

            AddScheduleCommand = new AddScheduleTypeCommand(
                new NavigationService<ScheduleTypeViewModel>(
                    navigationStore, () => new ScheduleTypeViewModel(navigationStore, userStore)));
            ResetFilter=new ResetFilterCommand(this);
        }
    }
}
