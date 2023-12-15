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

namespace SportShop.Windows
{
    /// <summary>
    /// Логика взаимодействия для UserUserWindow.xaml
    /// </summary>
    public partial class UserUserWindow : Window
    {
        public UserUserWindow()
        {
            InitializeComponent();
            AppData.frameUser = ProductFrame;
            ProductFrame.Navigate(new Views.GuestProductPage());
            LoginTB.Text = AppData.CurrentUser.Login.ToString();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductFrame.CanGoBack) ProductFrame.GoBack();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            AppData.CurrentUser = null;
            this.Close();
        }
    }
}
