using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly Connection _conn;
        private UserStore UserStore { get; }
        public string Login => UserStore.CurrentUser.Login;


        private List<Card> _cards;
        public List<Card> Cards
        {
            get => _cards;
            set => SetField(ref _cards, value, nameof(Cards));
        }

        public ICommand ExitCommand { get; }
        public ICommand AddRequestCommand { get; }
        public ICommand AddScheduleCommand { get; }
        public ICommand UpdateReestr { get; }
        public ICommand OpenScheduleCardCommand { get; }

        
        public ReestrViewModel(UserStore userStore, NavigationStore navigationStore)
        {
            _conn = new Connection();
            UserStore = userStore;
            Cards = _conn.ExecuteCards(UserStore.CurrentUser).ToList();

            OpenScheduleCardCommand = new OpenScheduleCardCommand(this,
                new ParameterNavigationService<Card, ScheduleTypeViewModel>(navigationStore,
                    (parameter) => new ScheduleTypeViewModel(navigationStore,userStore,parameter)));
            UpdateReestr = new UpdateReestrCommand(this);

            ExitCommand =
                new ExitCommand(new NavigationService<LoginViewModel>(navigationStore,
                    () => new LoginViewModel(navigationStore, userStore)));
            //AddScheduleCommand =
            //    new AddScheduleTypeCommand(new NavigationService<ScheduleTypeViewModel>(navigationStore,
            //        () => new ScheduleTypeViewModel(navigationStore)));
            AddRequestCommand =
                new AddRequestTypeCommand(new NavigationService<RequestTypeViewModel>(navigationStore,
                    () => new RequestTypeViewModel(navigationStore,userStore)));

        }
    }
}
