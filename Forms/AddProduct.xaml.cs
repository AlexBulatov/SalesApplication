using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using NEB.Models;

namespace NEB.Forms
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public Product newProduct;
        private List<Product> products;

        public AddProduct()
        {
            InitializeComponent();
            products = new DbConnection().GetProducts();
            ProductListBox.ItemsSource = products;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newProduct = (Product) ProductListBox.SelectedItems[0];
            DialogResult = true;
            Close();
        }

        private void ProductText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (ProductText.Text.Trim() == "") ProductListBox.ItemsSource = products;
            else ProductListBox.ItemsSource = from product in products
                where product.Title.Contains(ProductText.Text)
                select product;
        }
    }
}
