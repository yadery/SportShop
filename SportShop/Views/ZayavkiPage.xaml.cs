using SportShop.@base;
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
    /// Логика взаимодействия для ZayavkiPage.xaml
    /// </summary>
    public partial class ZayavkiPage : Page
    {
        public ZayavkiPage()
        {
            InitializeComponent();
            Update();
        }

        public void Update()
        {
            var content = AppData.db.Request.ToList();
            LWZayavki.ItemsSource = null;
            LWZayavki.ItemsSource = content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var currentZayavka = (sender as Button).DataContext as Request;
            if (MessageBox.Show("Вы уверены что хотите удалить заявку?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                AppData.db.Request.Remove(currentZayavka);
                AppData.db.SaveChanges();
                Update();
            }
        }
    }
}
