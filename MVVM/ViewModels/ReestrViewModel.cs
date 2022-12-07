using System;
using System.Collections.Generic;
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
        public  TUser User { get; }
        public string Login => User?.Login;

        private List<Card> _cards;
        public List<Card> Cards
        {
            get
            {
                return _cards;
            }
            set
            {
                _cards=value;
                OnPropertyChanged(nameof(Cards));
            }
        }

        public ICommand ExitCommand { get; }
        public ICommand AddRequestCommand { get; }
        public ICommand AddScheduleCommand { get; }
        public ICommand UpdateReestr { get; }


        public ReestrViewModel(TUser user,NavigationStore navigationStore)
        {
            _conn=new Connection();
            User = user;
            Cards = _conn.ExecuteCards(User).ToList();
            UpdateReestr = new UpdateReestrCommand(this);

            ExitCommand =
                new ExitCommand(new NavigationService<LoginViewModel>(navigationStore,
                    () => new LoginViewModel(navigationStore)));
            AddScheduleCommand =
                new AddScheduleTypeCommand(new NavigationService<ScheduleTypeViewModel>(navigationStore,
                    () => new ScheduleTypeViewModel(navigationStore)));
            AddRequestCommand =
                new AddRequestTypeCommand(new NavigationService<RequestTypeViewModel>(navigationStore,
                    () => new RequestTypeViewModel(navigationStore)));
        }

        public ReestrViewModel(NavigationStore navigationStore)
        {
            ExitCommand =
                new ExitCommand(new NavigationService<LoginViewModel>(navigationStore,
                    () => new LoginViewModel(navigationStore)));
            AddScheduleCommand =
                new AddScheduleTypeCommand(new NavigationService<ScheduleTypeViewModel>(navigationStore,
                    () => new ScheduleTypeViewModel(navigationStore)));
            AddRequestCommand =
                new AddRequestTypeCommand(new NavigationService<RequestTypeViewModel>(navigationStore,
                    () => new RequestTypeViewModel(navigationStore)));
            
        }
    }
}
