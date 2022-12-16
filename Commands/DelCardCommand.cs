using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.View;
using PIS8_2.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PIS8_2.Commands
{
    internal class DelCardCommand : Command
    {
        private readonly ReestrViewModel _viewModel;
        public DelCardCommand(ReestrViewModel viewModel)
        {
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

            using (var db = new TrappinganimalsContext())
            {
                foreach (var i in idCards)
                {
                    db.Cards.Remove(db.Cards.FirstOrDefault(c => c.Id == i));
                }
                db.SaveChanges();
            }
            _viewModel.UpdateReestr.Execute(null);
        }
    }
}
