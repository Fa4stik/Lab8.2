using System;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment;
using Spire.Doc;
using Document = Spire.Doc.Document;
using Table = Spire.Doc.Table;
using Section = Spire.Doc.Section;
using PageOrientation = Spire.Doc.Documents.PageOrientation;
using Paragraph = Spire.Doc.Documents.Paragraph;
using TextRange = Spire.Doc.Fields.TextRange;
using System.Globalization;
using Spire.Doc.Documents;

namespace PIS8_2.MVVM.Model.Export
{
    internal class ExportWordSchedule
    {
        public Document GenerateReport(Card card)
        {
            //Create a Document object
            Document doc = new Document();

            //Add a section
            Section s = doc.AddSection();

            // Ориентация страницы
            s.PageSetup.Orientation = PageOrientation.Landscape;

            // Шапка
            Paragraph header = s.AddParagraph();
            header.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange headerTR = header.AppendText($"\n\nЗаказ-наряд № {card.Numworkorder}");
            headerTR.CharacterFormat.Bold = true;
            headerTR.CharacterFormat.FontSize = 12;

            AddNextLine(s, 2);

            // Отображение обводки таблицы
            Table table = s.AddTable(true);

            // Добавление размеров таблицы
            table.ResetCells(5, 2);

            // Ширина колонок + в чём измеряются
            table.SetColumnWidth(0, 200, CellWidthType.Point);
            table.SetColumnWidth(1, 200, CellWidthType.Point);

            AddHeader(table, 0, "Дата отлова", HorizontalAlignment.Left, card.Datetrapping.ToString("D",
                              CultureInfo.CreateSpecificCulture("ru-RU")));
            AddHeader(table, 1, "Место отлова:", HorizontalAlignment.Left);
            AddHeader(table, 2, "Муниципальное образование", HorizontalAlignment.Right, card.IdMunicipNavigation.Namemunicip);
            AddHeader(table, 3, "Населённый пункт", HorizontalAlignment.Right, card.Locality);
            AddHeader(table, 4, "Адрес места отлова", HorizontalAlignment.Right, card.Adresstrapping);

            AddNextLine(s, 1);

            Table table2 = s.AddTable(false);
            table2.ResetCells(8, 4);
            table2.SetColumnWidth(0, 50, CellWidthType.Point);
            table2.SetColumnWidth(1, 350, CellWidthType.Point);
            table2.SetColumnWidth(2, 50, CellWidthType.Point);
            table2.SetColumnWidth(3, 350, CellWidthType.Point);

            // Заказчик
            AddCustemer(card, table2, doc);

            for (int i = 0; i < table2.Rows.Count-2; i++)
                for (int j = 0; j < table2.Rows[0].Cells.Count; j++)
                    if ((j==1 || j==3) && i!=1)
                        table2.Rows[i].Cells[j].CellFormat.Borders.Bottom.BorderType 
                            = Spire.Doc.Documents.BorderStyle.Thick;

            return doc;
        }

        public void SaveToWord(Document doc)
        {
            var saveDialog = new Microsoft.Win32.SaveFileDialog();
            saveDialog.FileName = "WordSchedule.docx";
            saveDialog.DefaultExt = ".docx";
            saveDialog.Filter = "Word documents (.docx)|*.docx";
            saveDialog.ShowDialog();
            try
            {
                doc.SaveToFile(saveDialog.FileName, FileFormat.Docx2013);
            }
            catch
            {
                MessageBox.Show("Закройте файл, перед тем как сохранить!");
            }
        }

        private static void AddNextLine(Section s, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Paragraph nextLine = s.AddParagraph();
                TextRange nlTr = nextLine.AppendText("\n");
            }
        }

        private static void AddHeader(Table table, int rw, string text, HorizontalAlignment hA, string textForHeader = "")
        {
            // Header
            Paragraph p1 = table.Rows[rw].Cells[0].AddParagraph();
            p1.Format.HorizontalAlignment = hA;
            TextRange TR1 = p1.AppendText($"{text}");
            TR1.CharacterFormat.FontName = "Calibri (Основной текст)";
            TR1.CharacterFormat.FontSize = 10;

            // Other
            Paragraph p2 = table.Rows[rw].Cells[1].AddParagraph();
            TextRange TR2 = p2.AppendText($"{textForHeader}");
            TR2.CharacterFormat.FontName = "Calibri (Основной текст)";
            TR2.CharacterFormat.FontSize = 10;
        }

        private void AddCustemer(Card card, Table table2, Document doc)
        {
            var omsu = card.IdOmsuNavigation;
            var org = card.IdOrgNavigation;

            ParagraphStyle styleFS11 = new ParagraphStyle(doc);
            ParagraphStyle styleFS9 = new ParagraphStyle(doc);

            styleFS9.CharacterFormat.FontName = "Times New Roman";
            styleFS9.CharacterFormat.FontSize = 9;
            styleFS11.CharacterFormat.FontName = "Times New Roman";
            styleFS11.CharacterFormat.FontSize = 11;

            doc.Styles.Add(styleFS9);
            doc.Styles.Add(styleFS11);

            AddCustomerStyles(0, 0, "Заказчик:", table2, doc, styleFS11);
            AddCustomerStyles(0, 1, $"{omsu.Nameomsu}", table2, doc, styleFS11);
            AddCustomerStyles(0, 2, $"Исполнитель:", table2, doc, styleFS11);
            AddCustomerStyles(0, 3, $"{org.Nameorg}", table2, doc, styleFS11);

            AddCustomerStyles(1, 0, $"", table2, doc, styleFS9);
            AddCustomerStyles(1, 1, $"(название организации)", table2, doc, styleFS9);
            AddCustomerStyles(1, 2, $"", table2, doc, styleFS9);
            AddCustomerStyles(1, 3, $"(название организации)", table2, doc, styleFS9);

            AddCustomerStyles(2, 0, $"Адрес:", table2, doc, styleFS11);
            AddCustomerStyles(2, 1, $"{omsu.Adress}", table2, doc, styleFS11);
            AddCustomerStyles(2, 2, $"Адрес:", table2, doc, styleFS11);
            AddCustomerStyles(2, 3, $"{org.Adress}", table2, doc, styleFS11);

            AddCustomerStyles(3, 0, $"Телефон:", table2, doc, styleFS11);
            AddCustomerStyles(3, 1, $"{omsu.Phonenumber}", table2, doc, styleFS11);
            AddCustomerStyles(3, 2, $"Телефон:", table2, doc, styleFS11);
            AddCustomerStyles(3, 3, $"{org.Phonenumber}", table2, doc, styleFS11);

            AddCustomerStyles(4, 0, $"Ф.И.О.", table2, doc, styleFS11);
            AddCustomerStyles(4, 1, $"{omsu.Firstnamedir} {omsu.Surnamedir} {omsu.Patronymicdir}", table2, doc, styleFS11);
            AddCustomerStyles(4, 2, $"Ф.И.О.", table2, doc, styleFS11);
            AddCustomerStyles(4, 3, $"{org.Firstnamedir} {org.Surnamedir} {org.Patronymicdir}", table2, doc, styleFS11);

            AddCustomerStyles(5, 0, $"Подпись", table2, doc, styleFS11);
            AddCustomerStyles(5, 1, $"", table2, doc, styleFS11);
            AddCustomerStyles(5, 2, $"Подпись", table2, doc, styleFS11);
            AddCustomerStyles(5, 3, $"", table2, doc, styleFS11);

            AddCustomerStyles(6, 0, $"", table2, doc, styleFS11);
            AddCustomerStyles(6, 1, $"", table2, doc, styleFS11);
            AddCustomerStyles(6, 2, $"", table2, doc, styleFS11);
            AddCustomerStyles(6, 3, $"", table2, doc, styleFS11);

            AddCustomerStyles(7, 0, $"М.П.", table2, doc, styleFS11);
            AddCustomerStyles(7, 1, $"", table2, doc, styleFS11);
            AddCustomerStyles(7, 2, $"М.П.", table2, doc, styleFS11);
            AddCustomerStyles(7, 3, $"", table2, doc, styleFS11);
        }

        private void AddCustomerStyles(int rw, int cl, string textCell, Table table2, Document doc, ParagraphStyle style)
        {
            Paragraph p1 = table2.Rows[rw].Cells[cl].AddParagraph();
            if (cl == 1 || cl == 3)
                p1.Format.HorizontalAlignment = HorizontalAlignment.Center;
            else
                p1.Format.HorizontalAlignment = HorizontalAlignment.Left;
            p1.AppendText(textCell);
            p1.ApplyStyle(style.Name);
        }
    }
}
