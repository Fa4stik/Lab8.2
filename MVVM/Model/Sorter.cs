using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PIS8_2.MVVM.Model
{
    public class Sorter:INotifyPropertyChanged
    {
        private int _numberSorting;

        public int NumberSorting
        {
            get => _numberSorting;
            set => SetField(ref _numberSorting, value, "NumberSorting");
        }
        public string PropetryName { get; set; }

        private Direction _direction;

        public Direction Direction
        {
            get => _direction;
            set => SetField(ref _direction, value, "Direction");
        }

        public string DisplayState
        {
            get => GetEnumAndNumberTostring(Direction,NumberSorting);
            set
            {
                OnPropertyChanged();
            }
        }

        public Sorter(int numberSorting,string propetryName, Direction direction=Direction.None)
        {
            NumberSorting=numberSorting;
            PropetryName=propetryName;
            Direction=direction;
        }

        /// <summary>
        /// Графическая часть сортировки. Отвечает за переключение стрелочек-символов
        /// </summary>
        /// <param name="direction">Тип сортировки</param>
        /// <param name="num">Приоритет сортировки. Приоритетнее та, чей номер меньше</param>
        /// <returns></returns>
        public string GetEnumAndNumberTostring(Direction direction, int num)
        {
            var resultString = string.Empty;
            switch (direction)
            {
                case Direction.Ascending:
                    resultString += " ▲ ";
                    break;
                case Direction.Descending:
                    resultString += " ▼ ";
                    break;
                default:
                    return "";
            }
            return resultString+num;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

    public enum Direction
    {
        None,
        Ascending,
        Descending
    }
}
