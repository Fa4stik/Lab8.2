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

    public partial class Authorization : Window
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
            byte[] hashByte = HashPassword(Password.Text);
            var hashString = HashToString(hashByte);
            using (var db = new trappinganimalsContext())
            {
                var user = db.Tusers.ToList();
                if (user.Exists(x => x.Login == Login.Text && x.Passwordhash == hashString))
                    new Reestr().Show();
                else
                    MessageBox.Show("Неверный логин/пароль");
            }
        }

        static byte[] HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            return SHA256.HashData(bytes);
        }

        static string HashToString(byte[] hash)
        {
            return Convert.ToHexString(hash).ToLower();
        }
    }
}
