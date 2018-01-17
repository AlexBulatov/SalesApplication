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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NEB.Forms;
using NEB.Models;

namespace NEB
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Sale> SaleCollection = new ObservableCollection<Sale>();
        DbConnection db = new DbConnection();

        public MainWindow()
        {
            InitializeComponent();
            SaleCollection = new ObservableCollection<Sale>(db.GetSales());
            SaleGrid.ItemsSource = SaleCollection;
            Sum.Content = db.GetTotalSum() + "р.";
        }

        private void AddSale_Click(object sender, RoutedEventArgs e)
        {
            AddSale form = new AddSale();
            if (form.ShowDialog() == true)
            {
                Sale addSale = new Sale((int)db.CreateSale(form.sale))
                {
                    Manager = form.sale.Manager,
                    Product = form.sale.Product,
                    SoldTime = form.sale.SoldTime,
                    SumMoney = form.sale.SumMoney
                };
                SaleCollection.Add(addSale);
            }
        }

        private void EditSale_Click(object sender, RoutedEventArgs e)
        {
            if (SaleGrid.SelectedItem != null)
            {
                UpdateSaleForm form = new UpdateSaleForm((Sale)SaleGrid.SelectedItem);
                if (form.ShowDialog() == true)
                {
                    db.UpdateSale(form.sale);
                    SaleCollection.RemoveAt(SaleGrid.SelectedIndex);
                    SaleCollection.Add(form.sale);
                }
            }

        }

        private void OpenCatalogs_Click(object sender, RoutedEventArgs e)
        {
            Catalogs form = new Catalogs();
            form.ShowDialog();
        }
    }
}
