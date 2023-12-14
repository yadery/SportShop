using Microsoft.Win32;
using SportShop.@base;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace SportShop.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        public Product currentProduct;
        private byte[] _mainImageData = null;
        public string img = null;
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Images\");
        public string selectefFileName;
        public string extension = "";

        public AddEditProductPage()
        {
            InitializeComponent();
            this.WindowTitle = "Добавление товара";
            var provider = AppData.db.Providers.Select(p => p.ProviderName).ToList();
            manufacturerBox.ItemsSource = provider;
            var category = AppData.db.Categories.Select(p => p.CategoryName).ToList();
            categoryBox.ItemsSource = category;
            var sizes = AppData.db.Size.Select(s => s.SizeName).ToList();
            sizeBox.ItemsSource = sizes;
        }
        public AddEditProductPage(Product product)
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

        private void selectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Фото | *.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                img = Path.GetFileName(ofd.FileName);
                extension = Path.GetExtension(img);
                selectefFileName = ofd.FileName;
                _mainImageData = File.ReadAllBytes(ofd.FileName);
                imageBox.Source = new ImageSourceConverter()
                    .ConvertFrom(_mainImageData) as ImageSource;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var category = AppData.db.Categories.Where(a => a.CategoryName == categoryBox.SelectedItem.ToString()).FirstOrDefault();
            var provider = AppData.db.Providers.Where(a => a.ProviderName == manufacturerBox.SelectedItem.ToString()).FirstOrDefault();
            var sizes = AppData.db.Size.Where(a => a.SizeName == sizeBox.SelectedItem.ToString()).FirstOrDefault();
            if (img != null)
            {
                img = ArticleBox.Text + extension;
                var files = Directory.GetFiles(path);
                int a = 0;
                while (File.Exists(path + img))
                {
                    a++;
                    img = ArticleBox.Text + $" ({a})" + extension;
                }
                path = path + img;
                File.Copy(selectefFileName, path);
            }
            //else if (currentTovar.image != null)
            //{
            //    img = currentTovar.image;
            //}
            if (currentProduct == null)
            {
                Product product = new Product()
                {
                    Article = Int32.Parse(ArticleBox.Text),
                    ProductName = nameBox.Text,
                    Description = descriptionBox.Text,
                    CategoryID = category.ID,
                    Price = Int32.Parse(priceBox.Text),
                    Count = Int32.Parse(amountInStock.Text),
                    SizeID = sizes.ID,
                    ProviderID = provider.ID,
                    
                    Image = img

                };
                AppData.db.Product.Add(product);
                AppData.db.SaveChanges();
                MessageBox.Show("Товар добавлен!");
            }
            else
            {
                currentProduct.Image = img;
                currentProduct.Article = Int32.Parse(ArticleBox.Text); 
                currentProduct.ProductName = nameBox.Text;
                currentProduct.Description = descriptionBox.Text;
                currentProduct.Price = Int32.Parse(priceBox.Text);
                currentProduct.ProviderID = provider.ID;
                currentProduct.ProviderID = category.ID;
                currentProduct.SizeID = sizes.ID;
                AppData.db.SaveChanges();
                MessageBox.Show("Товар обновлен!");
                currentProduct = null;
            }
            Windows.UserWindow window = new Windows.UserWindow();
            window.Show();
            Window.GetWindow(this).Close();
        }
    }
}
