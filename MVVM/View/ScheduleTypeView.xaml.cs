using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace PIS8_2.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для ScheduleTypeView.xaml
    /// </summary>
    public partial class ScheduleTypeView : UserControl
    {
        public ScheduleTypeView()
        {
            InitializeComponent();
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            {

                //open a pdf document

                OpenFileDialog dialog = new OpenFileDialog()
                {

                    Filter = "Pdf document(*.Pdf)|*.pdf",

                    Title = "Open Pdf Document",

                    Multiselect = false

                };

                bool? result = dialog.ShowDialog();

                if (result.Value)

                {

                    try

                    {

                        //Load pdf document from file.

                        this.pdfDocumentViewer1.LoadFromFile(dialog.FileName);

                    }

                    catch (Exception ex)

                    {

                        MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

                    }

                }

            }
        }
    }
}
