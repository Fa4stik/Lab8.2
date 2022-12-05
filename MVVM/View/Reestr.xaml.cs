using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PIS8_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public class ReestrInfo
    {
        public bool Check { get; set; } // выделение
        public int NumMK { get; set; } // номер МК
        public DateTime DataMK { get; set; } // дата МК
        public string Munic { get; set; } // муниципальное образование
        public string OMSU { get; set; } // ОМСУ
        public string ExecMK { get; set; } // исполнитель МК
        public int NumOrder { get; set; } // номер заказа
        public string Locality { get; set; } // населённый пункт
        public DateTime DataOrder { get; set; } // дата заказа
        public DateTime DataCapture { get; set; } // дата отлова
        public string PurposeCapture { get; set; } // цель отлова
        public string CaptureOrder { get; set; } // заявка на отлов или план-график

    }
    public partial class Reestr : Window, ICloseable
    {
        private BindingList<ReestrInfo> _reestrInfo;
        public Reestr()
        {
            InitializeComponent();
        }

        private void dgReestr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgReestr_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _reestrInfo = new BindingList<ReestrInfo>()
            {
                new ReestrInfo{ Check = true, NumMK = 12, DataMK = DateTime.Now, Munic = "Организация", OMSU = "ОМСУ", ExecMK = "Исполнитель", NumOrder=2341, Locality="Тюмень", DataOrder=DateTime.Now, DataCapture=DateTime.Now, PurposeCapture = "Цель отлова", CaptureOrder="План-график"},
                new ReestrInfo{ Check = true, NumMK = 1, DataMK = DateTime.Now, Munic = "Организация2", OMSU = "ОМСУ2", ExecMK = "Исполнитель2", NumOrder=3, Locality="Тюмень2", DataOrder=DateTime.Now, DataCapture=DateTime.Now, PurposeCapture = "Цель отлова2", CaptureOrder="Заказ-наряд"},
                new ReestrInfo{ Check = true, NumMK = 1, DataMK = DateTime.Now, Munic = "Организация3", OMSU = "ОМСУ3", ExecMK = "Исполнитель3", NumOrder=323, Locality="Тюмень3", DataOrder=DateTime.Now, DataCapture=DateTime.Now, PurposeCapture = "Цель отлова3", CaptureOrder="План-график"},
                new ReestrInfo{ Check = true, NumMK = 1, DataMK = DateTime.Now, Munic = "Организация4", OMSU = "ОМСУ4", ExecMK = "Исполнитель4", NumOrder=551, Locality="Тюмень4", DataOrder=DateTime.Now, DataCapture=DateTime.Now, PurposeCapture = "Цель отлова4", CaptureOrder="Заказ-наряд"}
            };
            dgReestr.ItemsSource = _reestrInfo;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new SelectTypeOrder().Show();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            ReestrInfo rsInfo = dgReestr.SelectedItem as ReestrInfo;
            if (rsInfo.CaptureOrder == "План-график")
                new ScheduleType().Show();
            else
                new RequestType().Show();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            int index = dgReestr.SelectedIndex;
            _reestrInfo.RemoveAt(index);
            dgReestr.ItemsSource = _reestrInfo;
        }
    }
}
