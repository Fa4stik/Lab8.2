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
    public partial class Reestr : Window
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
                new ReestrInfo{ Check = true, NumMK = 12, DataMK = DateTime.Now, Munic = "Организация", OMSU = "ОМСУ", ExecMK = "Исполнитель", NumOrder=2341, Locality="Тюмень", DataOrder=DateTime.Now, DataCapture=DateTime.Now, PurposeCapture = "Цель отлова", CaptureOrder="План-график"}
            };
            dgReestr.ItemsSource = _reestrInfo;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new SelectTypeOrder().Show();
        }
    }
}
