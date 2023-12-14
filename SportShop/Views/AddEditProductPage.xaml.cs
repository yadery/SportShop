using Microsoft.Win32;
using SportShop.@base;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SportShop.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        public Product currentProduct;
        public byte[] imageData = null;
        public string currentImage = null;
        public string extension = ".png";
        public string selectedFileName;
        public string path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName + $"/Resources/";
        public AddEditProductPage(Product product)
        {
            InitializeComponent();
            FillComboBoxes();
            if (product != null)
            {
                currentProduct = product;
                Title = "Редактирование товара";
                saveButton.Content = "Сохранить";
                FillData();
            }
            else
            {
                currentProduct = null;
                Title = "Добавление товара";
                saveButton.Content = "Добавить";
            }
        }
        private void FillComboBoxes()
        {
            categoryBox.ItemsSource = AppData.db.Categories.ToList().Select(c => c.CategoryName);
            manufacturerBox.ItemsSource = AppData.db.Providers.ToList().Select(m => m.ProviderName);            
        }
        private void FillData()
        {
            nameBox.Text = currentProduct.ProductName;
            descriptionBox.Text = currentProduct.Description;
            categoryBox.SelectedItem = AppData.db.Categories.Where(pt => pt.ID == currentProduct.CategoryID).First().CategoryName;
            priceBox.Text = currentProduct.Price.ToString();
            amountInStock.Text = currentProduct.Count.ToString();
            manufacturerBox.SelectedItem = AppData.db.Providers.Where(m => m.ID == currentProduct.ProviderID).First().ProviderName;           
            currentImage = currentProduct.Image;
            if (currentImage != null)
            {
                imageData = File.ReadAllBytes(path + currentImage);
                imageBox.Source = new ImageSourceConverter().ConvertFrom(imageData) as ImageSource;
            }
        }
        private void selectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Image | *.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                selectedFileName = ofd.FileName;
                currentImage = Path.GetFileName(selectedFileName);
                extension = Path.GetExtension(currentImage);
                imageData = File.ReadAllBytes(selectedFileName);
                imageBox.Source = new ImageSourceConverter().ConvertFrom(imageData) as ImageSource;
            }
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentImage != null)
            {
                currentImage = nameBox.Text + extension;

                int a = 0;
                while (File.Exists(path + currentImage))
                {
                    a++;
                    currentImage = $"{nameBox.Text}({a}){extension}";
                }
                File.Copy(selectedFileName, path + currentImage);
            }
            else if (currentProduct.Image != null)
            {
                currentImage = currentProduct.Image;
            }
            if (currentProduct != null)
            {
                currentProduct.ProductName = nameBox.Text;
                currentProduct.Description = descriptionBox.Text;
                currentProduct.CategoryID = AppData.db.Categories.Where(pt => pt.CategoryName == categoryBox.Text).First().ID;
                //currentProduct.Price = (decimal)(double.Parse(priceBox.Text));
                currentProduct.Count = int.Parse(amountInStock.Text);
                currentProduct.ProviderID = AppData.db.Providers.Where(m => m.ProviderName == manufacturerBox.Text).First().ID;                
                currentProduct.Image = currentImage;
                AppData.db.SaveChanges();
                MessageBox.Show("Товар успешно обновлен");
            }
            else
            {
                Product product = new Product();

                product.ProductName = nameBox.Text;
                product.Description = descriptionBox.Text;
                product.CategoryID = AppData.db.Categories.Where(pt => pt.CategoryName == categoryBox.Text).First().ID;
               // product.Price = (decimal)(double.Parse(priceBox.Text));
                product.Count = int.Parse(amountInStock.Text);
                product.ProviderID = AppData.db.Providers.Where(m => m.ProviderName == manufacturerBox.Text).First().ID;
               
                product.Image = currentImage;

                AppData.db.Product.Add(product);
                AppData.db.SaveChanges();
                MessageBox.Show("Товар успешно добавлен");
            }
            NavigationService.Navigate(new ProductPage());
        }
    }
}
