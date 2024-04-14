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
using DemoEx.Windows;

namespace DemoEx.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorizathion.xaml
    /// </summary>
    public partial class Authorizathion : Page
    {
        public Authorizathion()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            var user = App.context.Users.Where(u => u.login.ToString() == tbLogin.Text).FirstOrDefault();
            if (user != null)
            {
                if (user.password.ToString().ToLower() == pbPassword.Password.ToLower())
                {
                    App.curr_user.id_user = user.id_user;
                    App.curr_user.role = user.role;
                    HomeW homeW = new HomeW();
                    homeW.Show();
                    Window.GetWindow(this).Close();
                }
                else
                {
                    txtxError.Text = "Неправильный пароль";
                }
            }
            else
            {
                txtxError.Text = "Неправильный логин";
            }
        }

        private void pbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtxError.Text = "";
        }

        private void tbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtxError.Text = "";
        }
    }
}
