using PIS8_2.MVVM.Model;
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
    
    public partial class Reestr : Window
    {
        private BindingList<Card> _reestrInfo;
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
            using (var db = new trappinganimalsContext())
            {
                var cards = db.Cards.ToList();
                _reestrInfo = new BindingList<Card>(cards);
                dgReestr.ItemsSource = _reestrInfo;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new SelectTypeOrder().Show();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Card rsInfo = dgReestr.SelectedItem as Card;
            if (rsInfo.Targetorder == "План-график")
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
