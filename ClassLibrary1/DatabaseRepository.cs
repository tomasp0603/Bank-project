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
        public Customer RetrieveData(string user)
        {
            string cs = GenerateConnectionString();
            using (SqlConnection connection = new SqlConnection(cs))
            {
                Customer customer= new Customer();
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Customers where Username='" + user + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customer.User = reader.GetString(1);
                    customer.Password = reader.GetString(2);
                    customer.Id = reader.GetInt32(0);
                    customer.Balance= reader.GetInt32(3);
                }
                return customer;
            }
        }

        public bool CheckIfExists(string column, string valueToAsk)
        {
            string cs = GenerateConnectionString();
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Customers where " + column + "='" + valueToAsk + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public void ShowAllMovements(int id)
        {
            string cs = GenerateConnectionString();
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Movements where CustomerId=" + id + " order by Id desc";
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("Movements: ");
                while (reader.Read())
                {
                    Console.Write("Type: " + reader.GetString(1) + "   ");
                    Console.Write("Ammount: " + reader.GetInt32(2) + "    ");
                    Console.Write("Balance: " + reader.GetInt32(3) + "    ");
                    Console.WriteLine("Time of movement: " + reader.GetDateTime(5));
                }
            }
        }

        public Movement RetrieveLastMovement (int id)
        {
            string cs = GenerateConnectionString();
            using (SqlConnection connection = new SqlConnection(cs))
            {
                Movement movement = new Movement();
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select top 1 * from Movements where CustomerId=" + id + " order by Id desc";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    movement.Id = reader.GetInt32(0);
                    movement.Type = reader.GetString(1);
                    movement.AmmountOfMoney = reader.GetInt32(2);
                    movement.Balance = reader.GetInt32(3);
                    movement.CustomerId = id;
                    movement.DateTime = reader.GetDateTime(5);
                }
                return movement;
            }
        }

        public void UpdateData(Customer customer)
        {
            string cs = GenerateConnectionString();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "update Customers set Username='" + customer.User + "', Password='" + customer.Password + "', Balance=" + customer.Balance + " where Id=" + customer.Id;
                SqlDataReader reader = cmd.ExecuteReader();
            }
        }

        public void InsertMovement(Movement movement)
        {
            string cs = GenerateConnectionString();
            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "insert into Movements values ('" + movement.Type + "'," + movement.AmmountOfMoney + "," + movement.Balance + "," + movement.CustomerId + ",getdate())";
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
