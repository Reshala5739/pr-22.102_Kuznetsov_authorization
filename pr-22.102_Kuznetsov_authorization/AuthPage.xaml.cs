using pr_22._102_Kuznetsov_authorization.Model;
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

namespace pr_22._102_Kuznetsov_authorization
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text.Trim();
            string password = tbPassword.Password.Trim();
            KontrolEntities db = new KontrolEntities();

            var user = db.Auth.FirstOrDefault(x => x.Login == login && x.Password == password);

            if (user != null)
            {
                var Role = db.Roles.Find(user.RoleID);
                if (Role != null)
                {
                    Page newPage = null;
                    switch (Role.RoleID)
                    {
                        case 1:
                            NavigationService.Navigate(new User(user));
                            break;
                        case 2:
                            NavigationService.Navigate(new Admin(user));
                            break;
                        default:
                            MessageBox.Show("Должность не определена.");
                            return;
                    }
                    if (newPage != null)
                    {
                        MainFrame.Navigate(newPage);
                    }
                }
            }
            else
            {
                MessageBox.Show("Неверно введён логин или пароль");
            }
        }
    }
}
