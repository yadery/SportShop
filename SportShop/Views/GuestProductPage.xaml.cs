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
    /// Логика взаимодействия для UserProductPage.xaml
    /// </summary>
    public partial class GuestProductPage : Page
    {
        public GuestProductPage()
        {
            InitializeComponent();
            var categories = AppData.db.Categories.ToList();
            foreach (var category in categories)
            {
                filterBox.Items.Add(category);
            }
            filterBox.SelectedIndex = 0;
            sortBox.SelectedIndex = 0;
            Update();
        }

        public void Update()
        {
            var content = AppData.db.Product.ToList();

            switch (sortBox.SelectedIndex)
            {
                case 0:
                    content = content.OrderBy(c => c.ProductName).ToList();
                    break;
                case 1:
                    content = content.OrderByDescending(c => c.ProductName).ToList();
                    break;
                case 2:
                    content = content.OrderBy(c => c.Price).ToList();
                    break;
                case 3:
                    content = content.OrderByDescending(c => c.Price).ToList();
                    break;
            }

            if (filterBox.SelectedIndex != 0)
            {
                content = content.Where(c => c.Categories.CategoryName == filterBox.SelectedItem.ToString()).ToList();
            }

            content = content.Where(c => c.ProductName.ToLower().Contains(searchBox.Text.ToLower()) || c.Description.ToLower().Contains(searchBox.Text.ToLower())).ToList();

            var amount = AppData.db.Product.ToList().Count;
            if (amount == 0)
            {
                searchBox.Text = "Нет совпадений";
            }
            else
            {
                
            }

            LWProducts.ItemsSource = null;
            LWProducts.ItemsSource = content;
        }

        private void btnAddCart_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var currentProduct = button.DataContext as Product;
            NavigationService.Navigate(new RequestPage(currentProduct));
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

