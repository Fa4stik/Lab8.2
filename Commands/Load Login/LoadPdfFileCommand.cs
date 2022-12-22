using PIS8_2.MVVM.Model.Data;
using PIS8_2.MVVM.View;
using PIS8_2.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PIS8_2.Commands.Base;
using PdfViewer = Patagames.Pdf.Net.Controls.Wpf.PdfViewer;

namespace PIS8_2.Commands
{
    internal class LoadPdfFileCommand : Command
    {
        private readonly ScheduleTypeViewModel _scheduleTypeViewModel;
        private readonly Connection _conn;


        public LoadPdfFileCommand(ScheduleTypeViewModel scheduleTypeViewModel)
        {
            _scheduleTypeViewModel = scheduleTypeViewModel;
            _conn = new Connection();
        }

        public override bool CanExecute(object parameter) => true;
           

        public override void Execute(object parameter)
        {
            var a = parameter;
            var b = (PdfViewer)a;
            var file = _conn.GetFile(_scheduleTypeViewModel.Card.IdFile)!;
            if (file == null)
            {
                return;
            }
            //var memoryStream = new MemoryStream(file);
            b.LoadDocument(file);
            if (_scheduleTypeViewModel.IsEditMode == true)
            {
                return;
            }
            //var a = parameter;
            //var b = (PdfDocumentViewer)a;
            //var pdfDocumentViewer = (PdfDocumentViewer)parameter;

            ////var v = (PdfDocumentViewer)b.OriginalSource;
            ////var c = (PdfDocumentViewer)b.Source;
            ////var file = _conn.GetFile(_scheduleTypeViewModel.Card.IdFile)!;
            //if (file == null)
            //{
            //    return;
            //}
            //var memoryStream = new MemoryStream(file);
            //pdfDocumentViewer.LoadFromStream(memoryStream);
            //var a = parameter.GetType();
            //MessageBox.Show("123123123");
        }
    }
}