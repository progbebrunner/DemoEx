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
using DemoEx.Pages;

namespace DemoEx.Windows
{
    /// <summary>
    /// Логика взаимодействия для HomeW.xaml
    /// </summary>
    public partial class HomeW : Window
    {
        public HomeW()
        {
            InitializeComponent();
            HomeWFr.Navigate(new RequestsP());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (HomeWFr.CanGoBack)
            {
                HomeWFr.GoBack();
            }
        }
    }
}
