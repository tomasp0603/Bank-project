using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class DatabaseRepository : IDatabase
    {
        public Customer RetrieveData(string pin)
        {
            string cs = GenerateConnectionString();
            using (SqlConnection connection = new SqlConnection(cs))
            {
                Customer customer = new Customer();
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Customers where Pin='" + pin + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customer.Id = reader.GetInt32(0);
                    customer.Pin = reader.GetString(1);
                    customer.Balance = reader.GetInt32(2);
                }
                return customer;
            }
        }

        public Customer RetrieveData(int id)
        {
            string cs = GenerateConnectionString();
            using (SqlConnection connection = new SqlConnection(cs))
            {
                Customer customer=new Customer();
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Customers where Id=" + id;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customer.Id = reader.GetInt32(0);
                    customer.Pin = reader.GetString(1);
                    customer.Balance = reader.GetInt32(2);
                }
                return customer;
            }
        }

        public void UpdateData(Customer customer)
        {
            string cs = GenerateConnectionString();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "update Customers set Balance=" + customer.Balance + " where Pin='" + customer.Pin + "'";
                SqlDataReader reader = cmd.ExecuteReader();
            }
        }

        public string GenerateConnectionString()
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            connectionString.DataSource = "ABCNBK397";
            connectionString.IntegratedSecurity = true;
            connectionString.InitialCatalog = "Bank1";
            string cs = connectionString.ConnectionString;
            return cs;
        }
    }
}
