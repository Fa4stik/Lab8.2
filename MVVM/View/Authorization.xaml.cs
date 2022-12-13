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
using System.Windows.Shapes;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace PIS8_2
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public interface ICloseable
    {
        void Close();
    }

    public partial class Authorization : Window, ICloseable
    {


        public Authorization()
        {
            InitializeComponent();
        }

        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}