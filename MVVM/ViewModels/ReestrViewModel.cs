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
        private UserStore UserStore { get; }
        public string Login => UserStore.CurrentUser.Login;


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

        
        public ReestrViewModel(UserStore userStore, NavigationStore navigationStore)
        {
            UserStore = userStore;


            UpdateReestr = new UpdateReestrCommand(this, userStore);
            UpdateReestr.Execute(null);

            OpenScheduleCardCommand = new OpenScheduleCardCommand(this,
                new ParameterNavigationService<Card, ScheduleTypeViewModel>(navigationStore,
                    (parameter) => new ScheduleTypeViewModel(navigationStore,userStore,parameter)));
            

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

        }
    }
}
