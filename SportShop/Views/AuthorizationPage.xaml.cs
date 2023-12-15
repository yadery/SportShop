using SportShop.Windows;
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

namespace SportShop.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }
        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(TBLogin.Text) || String.IsNullOrEmpty(TBPassword.Password))
                {
                    MessageBox.Show("Заполните поля!");
                    return;
                }

                var currentUser = AppData.db.Users.FirstOrDefault((u) => u.Login == TBLogin.Text && u.Password == TBPassword.Password);

                if (currentUser == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка авторизации");
                }
                else
                {
                    if (currentUser.Login.Equals(TBLogin.Text) && currentUser.Password.Equals(TBPassword.Password))
                    {
                        AppData.CurrentUser = currentUser;
                        if (currentUser.RoleID == 3 ) //admin check
                        {
                            UserWindow admin = new UserWindow();
                            admin.Show();
                            
                        }
                        else
                        {
                            UserUserWindow userGuest = new UserUserWindow();
                            userGuest.Show();
                        }
                        Window.GetWindow(this).Close();
                    }
                    else
                    {
                        MessageBox.Show("Введите корректные логин и пароль", "Ошибка авторизации");
                    }
                }
            }   
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка " + ex.Message.ToString());
            }
        }

        private void BtnSignInAsGuest_Click(object sender, RoutedEventArgs e)
        {
            GuestWindow guestWindow = new GuestWindow();
            guestWindow.Show();
            Window.GetWindow(this).Close();
        }
    }
}
