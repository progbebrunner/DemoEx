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

namespace DemoEx.Pages
{
    public partial class RequestsP : Page
    {
        //List<string> types_of_eq;
        public RequestsP()
        {
            InitializeComponent();
            cbxSort.SelectedIndex = 0;
            cbxFilter.SelectedIndex = 0;
            
            var types_of_eq = App.context.Types_of_equipment.Select(t => t.name).ToList();
            foreach (var type in types_of_eq)
            {
                cbxFilter.Items.Add(type);
            }
            ReqsLoad();
        }

        public void ReqsLoad()
        {
            tblMsg.Text = null;
            var reqs = App.context.Requests.ToList();
            if(App.curr_user.role == 1)
            {
                reqs = reqs.Where(r => r.client == App.curr_user.id_user).ToList(); 
            }

            if (cbxFilter.SelectedIndex > 0)
            {
                var id_eq = App.context.Types_of_equipment.Where(t => t.name == cbxFilter.SelectedValue.ToString()).FirstOrDefault();
                reqs = reqs.Where(r => r.type_of_eq.Equals(id_eq.id_type_of_eq)).ToList();
            }
                       
            switch (cbxSort.SelectedIndex)
            {
                case 0:
                    reqs = reqs.OrderBy(r => r.id_req).ToList();
                    break;
                case 1:
                    reqs = reqs.OrderByDescending(r => r.id_req).ToList();
                    break;
                case 2:
                    reqs = reqs.OrderBy(r => r.serial_number).ToList();
                    break;
                case 3:
                    reqs = reqs.OrderByDescending(r => r.serial_number).ToList();
                    break;
            }
            reqs = reqs.Where(r => r.description.ToLower().Contains(tbxSearch.Text.ToLower())).ToList();
            if (reqs.Count > 0)
            {
                LVReqs.ItemsSource = null;
                LVReqs.ItemsSource = reqs;
            }
            else
            {
                LVReqs.ItemsSource = null;
                tblMsg.Text = "Ничего не найдено";
            }
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReqsLoad();
        }

        private void cbxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReqsLoad();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {            
            NavigationService.Navigate(new AddEdit());
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (App.curr_user.role == 2)
            {
                var btn = (Button)sender;
                var req = btn.DataContext as Requests;
                NavigationService.Navigate(new AddEdit(req));
            }
            else
            {
                MessageBox.Show("Эта функция вам недоступна");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var req = button.DataContext as Requests;
            if (MessageBox.Show("Вы точно хотите удалить заявку?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.context.Requests.Remove(req);
                App.context.SaveChanges();
                ReqsLoad();
                MessageBox.Show("Заявка успешно удалена!");
            }
        }
    }
}
