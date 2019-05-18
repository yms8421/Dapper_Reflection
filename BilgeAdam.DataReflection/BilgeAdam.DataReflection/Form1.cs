using BilgeAdam.DataReflection.Helpers;
using BilgeAdam.DataReflection.Models;
using Dapper;
using System;
using System.Windows.Forms;

namespace BilgeAdam.DataReflection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var dh = new DataHelper();
            using (var connection = dh.GetConnection())
            {
                //dgvProducts.DataSource = dh.GetData(connection, "SELECT ProductName, UnitPrice, UnitsInStock FROM Products");
                //dgvEmployees.DataSource = dh.GetData(connection, "SELECT FirstName, LastName, HireDate FROM Employees");

                //dgvProducts.DataSource = dh.GetProducts();
                //dgvEmployees.DataSource = dh.GetEmployees();

                //dgvProducts.DataSource = dh.GetData<Product>("SELECT ProductName, UnitPrice, UnitsInStock FROM Products");
                //dgvEmployees.DataSource = dh.GetData<Employee>("SELECT FirstName, LastName, HireDate FROM Employees");
                //dgvCustomers.DataSource = dh.GetData<Customer>("SELECT TOP 10 * FROM Customers");

                dgvProducts.DataSource = connection.Query<Product>("SELECT ProductName, UnitPrice, UnitsInStock FROM Products");
                dgvEmployees.DataSource = connection.Query<Employee>("SELECT FirstName, LastName, HireDate FROM Employees");
                dgvCustomers.DataSource = connection.Query<Customer>("SELECT TOP 10 * FROM Customers");
            }
        }
    }
}
