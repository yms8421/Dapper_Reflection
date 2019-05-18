using BilgeAdam.DataReflection.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BilgeAdam.DataReflection.Helpers
{
    public class DataHelper
    {
        public SqlConnection GetConnection()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["NorthwindConnStr"].ConnectionString);
            connection.Open();
            return connection;
        }

        public DataTable GetData(SqlConnection connection, string query)
        {
            var cmd = new SqlCommand(query, connection);
            var rdr = cmd.ExecuteReader();
            var dt = new DataTable();
            dt.Load(rdr);
            return dt;
        }

        public IEnumerable<Product> GetProducts()
        {
            using (var conn = GetConnection())
            {
                var products = new List<Product>();
                var cmd = new SqlCommand("SELECT ProductName, UnitPrice, UnitsInStock FROM Products", conn);
                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var product = new Product
                    {
                        ProductName = rdr["ProductName"].ToString(),
                        UnitPrice = Convert.ToDecimal(rdr["UnitPrice"]),
                        UnitsInStock = Convert.ToInt32(rdr["UnitsInStock"])
                    };
                    products.Add(product);
                }
                return products;
            }
        }

        public IEnumerable<Employee> GetEmployees()
        {
            using (var conn = GetConnection())
            {
                var employees = new List<Employee>();
                var cmd = new SqlCommand("SELECT FirstName, LastName, HireDate FROM Employees", conn);
                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var product = new Employee
                    {
                        FirstName = rdr["FirstName"].ToString(),
                        LastName = rdr["LastName"].ToString(),
                        HireDate = Convert.ToDateTime(rdr["HireDate"])
                    };
                    employees.Add(product);
                }
                return employees;
            }
        }

        public IEnumerable<T> GetData<T>(string query)
        {
            using (var conn = GetConnection())
            {
                var data = new List<T>();
                var type = typeof(T);
                var cmd = new SqlCommand(query, conn);
                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var obj = Activator.CreateInstance<T>();

                    foreach (var property in type.GetProperties())
                    {
                        property.SetValue(obj, rdr[property.Name]);
                    }
                    data.Add(obj);
                }

                return data;
            }
        }
    }
}
