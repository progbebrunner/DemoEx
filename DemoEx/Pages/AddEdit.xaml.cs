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
    /// <summary>
    /// Логика взаимодействия для AddEdit.xaml
    /// </summary>
    public partial class AddEdit : Page
    {
        Requests curr_req;
        public AddEdit()
        {
            InitializeComponent();
            ReqsLoad();
        }

        public void ReqsLoad()
        {
            tblStatus.Visibility = Visibility.Collapsed;
            cbxStatus.Visibility = Visibility.Collapsed;
            tblWorker.Visibility = Visibility.Collapsed;
            cbxWorker.Visibility = Visibility.Collapsed;
            cbxEqType.ItemsSource = App.context.Types_of_equipment.Select(eq => eq.name).ToList();
            cbxPrType.ItemsSource = App.context.Types_of_problem.Select(p => p.name).ToList();
            cbxEqType.SelectedIndex = 0;
            cbxPrType.SelectedIndex = 0;
        }

        public AddEdit(Requests req)
        {
            InitializeComponent();
            ReqsLoad(req);
        }

        public void ReqsLoad(Requests req)
        {
            curr_req = req;
            tblEqType.Visibility = Visibility.Collapsed;
            cbxEqType.Visibility = Visibility.Collapsed;
            tblPrType.Visibility = Visibility.Collapsed;
            cbxPrType.Visibility = Visibility.Collapsed;
            cbxStatus.ItemsSource = App.context.Statuses_of_request.Select(s => s.name).ToList();
            cbxWorker.ItemsSource = App.context.Users.Where(u => u.role == 3).Select(u => u.login).ToList();
            cbxStatus.SelectedIndex = 0;
            cbxWorker.SelectedIndex = 0;
            tbxDesc.Text = req.description;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (curr_req != null)
            {
                //MessageBox.Show($"Index - {cbxStatus.SelectedIndex + 1} \nId - {App.context.Users.Where(u => u.login == cbxWorker.SelectedValue.ToString()).Select(u => u.id_user).FirstOrDefault()}");
                curr_req.status = cbxStatus.SelectedIndex + 1;
                curr_req.worker = App.context.Users.Where(u => u.login == cbxWorker.SelectedValue.ToString()).Select(u => u.id_user).FirstOrDefault();
                App.context.SaveChanges();
                MessageBox.Show("Данные по заявке успешно обновлены!");
                NavigationService.Navigate(new RequestsP());
            }
            else
            {
                var new_req = new Requests
                {
                    client = App.curr_user.id_user,
                    date = DateTime.Now,
                    type_of_eq = cbxEqType.SelectedIndex + 1,
                    type_of_problem = cbxPrType.SelectedIndex + 1,
                    description = tbxDesc.Text,
                    status = 1
                };
                App.context.Requests.Add(new_req);
                App.context.SaveChanges();
                MessageBox.Show("Заявка успешно создана!");
                NavigationService.Navigate(new RequestsP());
            }
        }
    }
}
