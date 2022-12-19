using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
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
                if (first.Direction == Direction.None)
                {
                    first.Direction = Direction.Ascending;
                    _viewModel.SortingList.First(c => c.PropetryName == column.SortMemberPath).Direction = Direction.Ascending;
                    _viewModel.SortingList.First(c => c.PropetryName == column.SortMemberPath).NumberSorting=_viewModel.SortingList.Max(c=>c.NumberSorting)+1;
                    foreach (var sorter in _viewModel.SortingList)
                    {
                        sorter.DisplayState += "";
                    }
                    //dataGridSortingEventArgs.Column.Header = ((string)dataGridSortingEventArgs.Column.Header)
                    //    .Split("▲")
                    //    .First();
                    //dataGridSortingEventArgs.Column.Header += $" ▼ {first.NumberSorting}";
                }
                else if (first.Direction == Direction.Ascending)
                {
                    _viewModel.SortingList.First(c => c.PropetryName == column.SortMemberPath).Direction = Direction.Descending;
                    foreach (var sorter in _viewModel.SortingList)
                    {
                        sorter.DisplayState += "";
                    }
                    //dataGridSortingEventArgs.Column.Header = ((string)dataGridSortingEventArgs.Column.Header)
                    //    .Split("▲")
                    //    .First();
                    //dataGridSortingEventArgs.Column.Header += $" ▼ {first.NumberSorting}";
                }
                else if(first.Direction == Direction.Descending)
                {
                    _viewModel.SortingList.First(c => c.PropetryName == column.SortMemberPath).Direction = Direction.None;
                    ReArangeSorterListNumSorting(first.NumberSorting);
                    _viewModel.SortingList.First(c => c.PropetryName == column.SortMemberPath).NumberSorting = 0;
                    //_viewModel.SortingList.Remove(first);
                    //dataGridSortingEventArgs.Column.Header = ((string) dataGridSortingEventArgs.Column.Header)
                    //    .Split("▼")
                    //    .First();

                    foreach (var sorter in _viewModel.SortingList)
                    {
                        sorter.DisplayState += "";
                    }
                }
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
