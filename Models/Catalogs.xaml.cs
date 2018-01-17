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

namespace NEB.Models
{
    /// <summary>
    /// Логика взаимодействия для Catalogs.xaml
    /// </summary>
    public partial class Catalogs : Window
    {
        DbConnection db = new DbConnection();

        private ObservableCollection<Product> products;
        private ObservableCollection<Manager> managers;


        public Catalogs()
        {
            InitializeComponent();
            products = new ObservableCollection<Product>(db.GetProducts());
            managers = new ObservableCollection<Manager>(db.GetManagers());
            ProductGrid.ItemsSource = products;
            ManagerGrid.ItemsSource = managers;
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductGrid.SelectedIndex != -1)
            {
                if (db.DeleteProduct((Product) ProductGrid.SelectedItem) == -1)
                {
                    MessageBox.Show("Этот товар нельзя удалить");
                    return;
                }
                products.RemoveAt(ProductGrid.SelectedIndex);
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            Decimal.TryParse(PriceBox.Text, out decimal Price);

            if (TitleBox.Text.Trim() == "" || Price <= 0) return;

            Product newProduct = new Product(null)
            {
                Title = TitleBox.Text,
                MinPrice = Price
            };

            newProduct = new Product((int) db.CreateProduct(newProduct))
            {
                MinPrice = newProduct.MinPrice,
                Title = newProduct.Title
            };

            products.Add(newProduct);
        }

        private void AddManager_Click(object sender, RoutedEventArgs e)
        {
            if (LastNameBox.Text.Trim() == "" || FirstnameBox.Text.Trim()=="") return;

            Manager newManager = new Manager(null)
            {
                FirstName = FirstnameBox.Text,
                LastName = LastNameBox.Text
            };

            newManager = new Manager((int)db.CreateManager(newManager))
            {
                FirstName = newManager.FirstName,
                LastName = newManager.LastName
            };

            managers.Add(newManager);
        }

        private void DeleteManager_Click(object sender, RoutedEventArgs e)
        {
            if( ManagerGrid.SelectedIndex != -1)
            {
                if (db.DeleteManager((Manager) ManagerGrid.SelectedItem) == -1)
                {
                    MessageBox.Show("Этого продавца нельзя удалить");
                    return;
                }
                managers.RemoveAt(ManagerGrid.SelectedIndex);
            }
        }
    }
}
