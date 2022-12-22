using System;
using System.Linq;
using System.Windows.Controls;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model;
using PIS8_2.MVVM.ViewModels;

namespace PIS8_2.Commands
{
    class SortingCommand:Command
    {
        private readonly ReestrViewModel _viewModel;

        public SortingCommand(ReestrViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override bool CanExecute(object parameter)=>true;

        public override void Execute(object parameter)
        {
            var dataGridSortingEventArgs = (DataGridSortingEventArgs)parameter;
            dataGridSortingEventArgs.Handled=true;
            var column=dataGridSortingEventArgs.Column;

            if (_viewModel.SortingList.Select(c=>c.PropetryName).Contains(column.SortMemberPath))
            {
                var first = _viewModel.SortingList.First(c => c.PropetryName == column.SortMemberPath);
                switch (first.Direction)
                {
                    case Direction.None:
                        first.Direction = Direction.Ascending;
                        _viewModel.SortingList.First(c => c.PropetryName == column.SortMemberPath).Direction = Direction.Ascending;
                        _viewModel.SortingList.First(c => c.PropetryName == column.SortMemberPath).NumberSorting=_viewModel.SortingList.Max(c=>c.NumberSorting)+1;
                        break;
                    case Direction.Ascending:
                        _viewModel.SortingList.First(c => c.PropetryName == column.SortMemberPath).Direction = Direction.Descending;
                        break;
                    case Direction.Descending:
                        _viewModel.SortingList.First(c => c.PropetryName == column.SortMemberPath).Direction = Direction.None;
                        ReArangeSorterListNumSorting(first.NumberSorting);
                        _viewModel.SortingList.First(c => c.PropetryName == column.SortMemberPath).NumberSorting = 0;
                        break;

                }
                UpdateDisplayState();
            }


            _viewModel.UpdateReestr.Execute(null);
            //else
            //{
            //    var sorter = new Sorter(_viewModel.SortingList.Count + 1, column.SortMemberPath, Direction.Ascending);
            //    _viewModel.SortingList.Add(sorter);
            //    //dataGridSortingEventArgs.Column.Header += $" ▲ {_viewModel.SortingList.Count}";

            //}
            
            

            //_viewModel.SortingList.Add(c);
            //var r = c.Column.SortDirection;
            //var t = c.Column;
            //var b = parameter.GetType();

        }

        private void UpdateDisplayState()
        {
            foreach (var sorter in _viewModel.SortingList)
            {
                sorter.DisplayState += "";
            }
        }

        private void ReArangeSorterListNumSorting(int numSorting)
        {
            //_viewModel.SortingList = _viewModel.SortingList.Where(c => c.NumberSorting > numSorting).Select(c=>new int(){}).ToList();
            foreach (var sorter in _viewModel.SortingList.Where(c=>c.NumberSorting>numSorting))
            {
                sorter.NumberSorting--;
            }
        }
    }
}
