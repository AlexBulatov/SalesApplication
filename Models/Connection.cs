using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Threading;

namespace NEB.Models
{
    public class DbConnection
    {
        public DbConnection(string connection)
        {
            Connection = new SqlConnection();
            ConnectionString = connection;
        }

        public DbConnection()
        {
            Connection =  new SqlConnection();
            ConnectionString = ConfigurationManager.ConnectionStrings["SalesConnection"].ConnectionString;
        }

        #region Properties
        public string ConnectionString
        {
            get => Connection.ConnectionString;
            set => Connection.ConnectionString = value;
        }

        public SqlConnection Connection { get; set; }

        #endregion

        #region Create
        public decimal CreateManager(Manager manager)
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("CreateManager", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Firstname", manager.FirstName));
            cmd.Parameters.Add(new SqlParameter("@Lastname", manager.LastName));
            var result = cmd.ExecuteScalar();
            Connection.Close();
            return (decimal) result;
        }

        public decimal CreateProduct(Product product)
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("CreateProduct", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Title", product.Title));
            cmd.Parameters.Add(new SqlParameter("@MinPrice", product.MinPrice));
            var result = cmd.ExecuteScalar();
            Connection.Close();
            return (decimal) result;
        }

        public decimal CreateSale(Sale sale)
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("CreateSale", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Price", sale.SumMoney));
            cmd.Parameters.Add(new SqlParameter("@SellDate", sale.SoldTime));
            cmd.Parameters.Add(new SqlParameter("@ManID", sale.Manager.ID));
            cmd.Parameters.Add(new SqlParameter("@ProdID", sale.Product.ID));
            var result = cmd.ExecuteScalar();
            
            Connection.Close();
            return (decimal) result;
        }
        #endregion

        #region Read
        public List<Manager> GetManagers()
        {
            List<Manager> managers = new List<Manager>();
            Connection.Open();

                SqlCommand cmd = new SqlCommand("GetManagers", Connection);
                cmd.CommandType = CommandType.StoredProcedure;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var manager = new Manager((int)reader["ID"]);
                    manager.FirstName = (string) reader["FirstName"];
                    manager.LastName = (string)reader["LastName"];
                    managers.Add(manager);
                }
                Connection.Close();

            return managers;
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            Connection.Open();

            SqlCommand cmd = new SqlCommand("GetProducts", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var product = new Product((int)reader["ID"]);
                product.Title = (string)reader["Title"];
                product.MinPrice = (decimal) reader["MinPrice"];
                products.Add(product);
            }

            Connection.Close();
            return products;
        }

        public List<Sale> GetSales()
        {
            List<Sale> sales = new List<Sale>();
            Connection.Open();

            SqlCommand cmd = new SqlCommand("GetSales", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var sale = new Sale((int)reader["ID"]);
                sale.SoldTime = (DateTime)reader["Sold_In"];
                sale.SumMoney = (decimal)reader["Price"];
                sale.Manager = new Manager((int)reader["ManagerID"])
                {
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"]
                };
                sale.Product = new Product((int)reader["ProductID"])
                {
                    Title = (string)reader["Title"],
                    MinPrice = (decimal)reader["MinPrice"]
                };
                sales.Add(sale);
            }

            Connection.Close();
            return sales;
        }

        public Sale GetSale(int id)
        {
            Sale sale = new Sale(id);
            bool alreadyOpened = true;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                alreadyOpened = false;
            }

            SqlCommand cmd = new SqlCommand("GetSale", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                sale.SoldTime = (DateTime)reader["Sold_In"];
                sale.SumMoney = (decimal)reader["Price"];
                sale.Manager = new Manager((int)reader["ManagerID"])
                {
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"]
                };
                sale.Product = new Product((int)reader["ProductID"])
                {
                    Title = (string)reader["Title"],
                    MinPrice = (decimal)reader["MinPrice"]
                };
            }
            else
            {
                throw new SqlNullValueException("No data on this ID");
            }

            if (!alreadyOpened)
            {
                Connection.Close();
            }
            return sale;
        }

        public Product GetProduct(int id)
        {
            Product product = new Product(id);
            bool alreadyOpened = true;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                alreadyOpened = false;
            }

            SqlCommand cmd = new SqlCommand("GetProduct", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                product.Title= (string)reader["Title"];
                product.MinPrice = (decimal)reader["MinPrice"];
            }
            else
            {
                throw new SqlNullValueException("No data on this ID");
            }

            if (!alreadyOpened)
            {
                Connection.Close();
            }
            return product;
        }

        public Manager GetManager(int id)
        {
            Manager manager = new Manager(id);
            bool alreadyOpened = true;
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
                alreadyOpened = false;
            }

            SqlCommand cmd = new SqlCommand("GetManager", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                manager.FirstName = (string)reader["FirstName"];
                manager.LastName = (string)reader["LastName"];
            }
            else
            {
                throw new SqlNullValueException("No data on this ID");
            }

            if (!alreadyOpened)
            {
                Connection.Close();
            }
            return manager;
        }

        public decimal GetTotalSum()
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("TotalSales", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            var result = cmd.ExecuteScalar();
            Connection.Close();
            return (decimal) result;
        }
        #endregion

        #region Update

        public void UpdateSale(Sale sale)
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("UpdateSale", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", sale.ID));
            cmd.Parameters.Add(new SqlParameter("@Price", sale.SumMoney));
            cmd.Parameters.Add(new SqlParameter("@SellDate", sale.SoldTime));
            cmd.Parameters.Add(new SqlParameter("@ManID", sale.Manager.ID));
            cmd.Parameters.Add(new SqlParameter("@ProdID", sale.Product.ID));
            cmd.ExecuteNonQuery();
            Connection.Close();
        }
        #endregion

        #region Delete

        public int DeleteManager(Manager manager)
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("DeleteManager", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", manager.ID));
            int result = (int) cmd.ExecuteScalar();
            Connection.Close();
            return result;
        }

        public int DeleteProduct(Product product)
        {
            Connection.Open();
            SqlCommand cmd = new SqlCommand("DeleteProduct", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", product.ID));
            int result = (int)cmd.ExecuteScalar();
            Connection.Close();
            return result;
        }

        #endregion
    }
}