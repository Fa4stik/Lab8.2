using OfficeOpenXml;
using OfficeOpenXml.Style;
using PIS8_2.Converters;
using PIS8_2.MVVM.Model.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace PIS8_2.MVVM.Model.ExportExcel
{
    internal class ExportExcelReestr
    {
        /// <summary>
        /// Метод генерирует документ в формате .xls (MS Excel) с переданными параметрыми
        /// </summary>
        /// <param name="user">Текущий пользователь, необходим для фильра</param>
        /// <param name="filter">Параметры фильтра реестра</param>
        /// <param name="sorterParams">Параметры сортировки столбцов реестра</param>
        /// <returns>Возвращает документ в формате .xls с применёнными фильтрами / сортировкой</returns>
        public byte[] GenerateReport(Tuser user, FilterModel filter, List<Sorter> sorterParams)
        {
            var card = new Connection().ExecuteCardsWithFilter(user, filter, sorterParams);
            var package = new ExcelPackage();

            var item = ConverterCardsToLimitedCards.ConvertCardsToLimitedCards(card);
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
                foreach (var column in row.GetType().GetProperties().Skip(2))
                {
                    var value = column.GetValue(row);
                    if (value is DateTime)
                        value = Convert.ToDateTime(value).ToString("dd.MM.yyyy");
                    if (value is Enum)
                        value = (value as Enum).DisplayName();
                    sheet.Cells[i, j].Value = value;
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

        /// <summary>
        /// Сохраняет сгенерированный документ на ПК текущего пользователя
        /// </summary>
        /// <param name="reportExcel">Сгенированный документ в форма .xls (MS Excel)</param>
        public void SaveToExcel(byte[] reportExcel)
        {
            var saveDialog = new Microsoft.Win32.SaveFileDialog();
            saveDialog.FileName = "ExcelReestr.xlsx";
            saveDialog.DefaultExt = ".xlsx";
            saveDialog.Filter = "Excel documents (.xlsx)|*.xlsx";
            saveDialog.ShowDialog();
            try
            {
                System.IO.File.WriteAllBytes(saveDialog.FileName, reportExcel);
            }
            catch
            {
                MessageBox.Show("Закройте файл, перед тем как сохранить!");
            }
        }
    }

    public static class EnumExtensions
    {
        // Note that we never need to expire these cache items, so we just use ConcurrentDictionary rather than MemoryCache
        private static readonly
            ConcurrentDictionary<string, string> DisplayNameCache = new ConcurrentDictionary<string, string>();

        public static string DisplayName(this Enum value)
        {
            var key = $"{value.GetType().FullName}.{value}";

            var displayName = DisplayNameCache.GetOrAdd(key, x =>
            {
                var name = (DescriptionAttribute[])value
                    .GetType()
                    .GetTypeInfo()
                    .GetField(value.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), false);

                return name.Length > 0 ? name[0].Description : value.ToString();
            });

            return displayName;
        }
    }
}
