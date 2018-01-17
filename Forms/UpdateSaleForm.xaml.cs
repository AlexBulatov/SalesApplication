using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для UpdateSaleForm.xaml
    /// </summary>
    public partial class UpdateSaleForm : Window
    {
        public Sale sale;
        public UpdateSaleForm(Sale oldSale)
        {
            InitializeComponent();
            var list = new DbConnection().GetManagers(); 
            Seller.ItemsSource = list;

            this.sale = oldSale;
            PriceBox.Text = sale.SumMoney.ToString(CultureInfo.CurrentCulture);
            SellTimePicker.SelectedDate = sale.SoldTime;
            SelectedProductBox.Text = sale.Product.Title;
 
            var item = list.IndexOf(list.First(x => x.ID == oldSale.Manager.ID)); //NEED TO FIX LATER
            Seller.SelectedIndex = item;
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

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            sale.SoldTime = SellTimePicker.SelectedDate ?? DateTime.Now;
            Decimal.TryParse(PriceBox.Text, out decimal SumMoney);
            sale.SumMoney = SumMoney;
            DialogResult = true;
            Close();
        }

        private void Seller_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sale.Manager = (Manager)Seller.SelectedItem;
        }

        private void SellTimePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            sale.SoldTime = SellTimePicker.DisplayDate;
        }
    }
}
