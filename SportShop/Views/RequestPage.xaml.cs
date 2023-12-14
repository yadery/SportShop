using SportShop.@base;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace SportShop.Views
{
    /// <summary>
    /// Логика взаимодействия для RequestPage.xaml
    /// </summary>
    public partial class RequestPage : Page
    {
        public Product currentProduct;
        private byte[] _mainImageData = null;
        public string img = null;
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Images\");
        public string selectefFileName;
        public string extension = "";
        public RequestPage(Product product)
        {
            InitializeComponent();
            currentProduct = product;
            this.WindowTitle = "Редактирование товара";

            ArticleBox.Text = currentProduct.Article.ToString();
            nameBox.Text = currentProduct.ProductName.ToString();
            descriptionBox.Text = currentProduct.Description.ToString();
            categoryBox.SelectedItem = currentProduct.Categories.CategoryName;
            priceBox.Text = currentProduct.Price.ToString();
            amountInStock.Text = currentProduct.Count.ToString();
            manufacturerBox.SelectedItem = currentProduct.Providers.ProviderName;
            sizeBox.SelectedItem = currentProduct.Size.SizeName;
            if (currentProduct.Image != null)
            {
                _mainImageData = File.ReadAllBytes(path + currentProduct.Image);
                imageBox.Source = new ImageSourceConverter().ConvertFrom(_mainImageData) as ImageSource;
            }

            var provider = AppData.db.Providers.Select(p => p.ProviderName).ToList();
            manufacturerBox.ItemsSource = provider;
            var category = AppData.db.Categories.Select(p => p.CategoryName).ToList();
            categoryBox.ItemsSource = category;
            var sizes = AppData.db.Size.Select(s => s.SizeName).ToList();
            sizeBox.ItemsSource = sizes;
        }

        private void requestProductBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Request request = new Request()
            {
                userID = AppData.CurrentUser.ID,
                productArticle = currentProduct.Article,

            };
            AppData.db.Request.Add(request);
            AppData.db.SaveChanges();
            MessageBox.Show("Заявка отправлена!");
        }
    }
}
