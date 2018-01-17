using System;
using System.Globalization;
using System.Windows;
using NEB.Models;

namespace NEB.Forms
{
    /// <summary>
    /// Логика взаимодействия для AddSale.xaml
    /// </summary>
    public partial class AddSale : Window
    {
        public Sale sale = new Sale(null)
        {
            Manager = null,
            SoldTime = DateTime.Now,
            Product = null,
            SumMoney = 0
        };

        public AddSale()
        {
            InitializeComponent();
            Seller.ItemsSource = new DbConnection().GetManagers();
            PriceBox.Text = "0";
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            sale.SoldTime = SellTimePicker.SelectedDate ?? DateTime.Now;
            Decimal.TryParse(PriceBox.Text, out decimal SumMoney);
            sale.SumMoney = SumMoney;
            DialogResult = true;
            Close();
        }

        private void Seller_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            sale.Manager = (Manager) Seller.SelectedItem;
        }

        private void SellTimePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            sale.SoldTime = SellTimePicker.DisplayDate;
        }

        private void SelectProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProduct form = new AddProduct();
            if (form.ShowDialog() == true)
            {
                SelectedProductBox.Text = form.newProduct.ToString();
                sale.Product = form.newProduct;
                PriceBox.Text = form.newProduct.MinPrice.ToString(CultureInfo.CurrentCulture);
            }
        }
    }
}
