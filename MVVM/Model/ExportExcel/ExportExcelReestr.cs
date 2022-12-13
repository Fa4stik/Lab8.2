using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace PIS8_2.MVVM.Model.ExportExcel
{
    internal class ExportExcelReestr
    {
        public byte[] GenerateReport(List<LimitedCard> item)
        {
            var package = new ExcelPackage();

            // Add sheet
            var sheet = package.Workbook.Worksheets.Add("Реестр заявок");

            // Header excel
            string[] header =  { "Номер МК", "Дата заключения", "Муниципальное образование",
            "ОМСУ", "Исполнитель МК", "Номер заказ-наряда", "Населённый пункт", "Дата выдачи заказ-наряда",
            "Дата отлова", "Цель отлова", "Тип отлова"};
            sheet.Cells[1, 1].LoadFromArrays(new object[][] { header });

            // Style line and text
            FullBorderFillThin(1, 1, item.Count+1, header.Length, sheet);
            sheet.Cells[1,1,1, header.Length].Style.Font.Bold = true;

            // Fill cell
            var i = 2;
            foreach (var row in item)
            {
                var j = 1;
                foreach (var column in row.GetType().GetProperties().Skip(1))
                {
                    sheet.Cells[i, j].Value = column.GetValue(row);
                    j++;
                }
                i++;
            }

            // Auto fit column
            sheet.Cells[1, 1, item.Count, header.Length].AutoFitColumns();
            return package.GetAsByteArray();
        }

        private void FullBorderFillThin(int fromRow, int fromCol, int toRow, int toCol, ExcelWorksheet sheet)
        {
            var styleHeader = sheet.Cells[fromRow, fromCol, toRow, toCol].Style;
            styleHeader.Border.Top.Style = ExcelBorderStyle.Thin;
            styleHeader.Border.Bottom.Style = ExcelBorderStyle.Thin;
            styleHeader.Border.Left.Style = ExcelBorderStyle.Thin;
            styleHeader.Border.Right.Style = ExcelBorderStyle.Thin;
        }

        public void SaveToExcel(byte[] reportExcel)
        {
            var saveDialog = new Microsoft.Win32.SaveFileDialog();
            saveDialog.FileName = "ExcelReestr.xlsx";
            saveDialog.DefaultExt = ".xlsx";
            saveDialog.Filter = "Excel documents (.xlsx)|*.xlsx";
            saveDialog.ShowDialog();
            File.WriteAllBytes(saveDialog.FileName, reportExcel);
        }
    }
}
