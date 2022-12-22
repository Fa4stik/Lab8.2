using Microsoft.Win32;
using PIS8_2.Commands.Base;
using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.ViewModels;
using System.IO;
using System.Windows;

namespace PIS8_2.Commands.Load
{
    internal class DownloadFileCommand : Command
    {
        private readonly ScheduleTypeViewModel _scheduleTypeViewModel;
        private readonly Connection _conn;

        public DownloadFileCommand(ScheduleTypeViewModel scheduleTypeViewModel)
        {
            _scheduleTypeViewModel = scheduleTypeViewModel;
            _conn = new Connection();
        }

        public override bool CanExecute(object parameter) => true;

        /// <summary>
        /// Обработчик нажатия на кнопку "Загрузить"
        /// </summary>
        /// <param name="parameter">Собитие или кнопка, которая передаётся</param>
        public override void Execute(object parameter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "pdf files (*.pdf)|*.pdf",
                FilterIndex = 2,
                RestoreDirectory = true
            };
            string fileName = null;

            if (openFileDialog.ShowDialog() == true)
            {
                var filePath = openFileDialog.FileName;
                fileName = Path.GetFileName(filePath);
                var file = System.IO.File.ReadAllBytes(filePath);
                MessageBox.Show(file.Length.ToString());
                if (file.Length / (1024 * 1024) > 5)
                {
                    MessageBox.Show("Размер превышает 5 МБ. Загрузите другой файл");
                    return;
                }
                _conn.AddFile(_scheduleTypeViewModel.Card.IdFile, fileName, file);
            }


            _scheduleTypeViewModel.Card.IdFileNavigation.Name = fileName;
            _scheduleTypeViewModel.Card = _scheduleTypeViewModel.Card;
            if (!string.IsNullOrEmpty(_scheduleTypeViewModel.Card.IdFileNavigation.Name))
                _scheduleTypeViewModel.CheckModeDeleteVisibility = Visibility.Visible;
        }
    }
}
