using Supermatket.Modul;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermatket.Services
{
    internal class ProductDbService
    {
        public void CreateProduct(int category_id, string name, decimal price)
        {
            string command = $"INSERT INTO Product (category_id,Product_Name, Price) VALUES ('{category_id}','{name}',{price})";

            DataAccessLayer.ExecuteNonQuery(command);

        }
        public void UpdateProduct(int id, string newName, decimal newPrice)
        {
            string command = $"UPDATE dbo.Product" +
                    $" SET ProductName = '{newName}', Price = {newPrice}" +
                    $" WHERE Id = {id};";
            DataAccessLayer.ExecuteNonQuery (command);
        }
        public void DeleteProduct(int id)
        {
            string command = $"DELETE dbo.Product WHERE Id = {id}";
            
            DataAccessLayer.ExecuteNonQuery (command);
        }
        public void GetAllProducs()
        {
            string query = "SELECT * FROM dbo.Product;";

            ExecuteQuery(query);
        }
        public void GetProductById(int id)
        {
            string query = "SELECT * FROM dbo.Product" +
                $" WHERE Id = {id};";

            ExecuteQuery(query);
        }
        public void GetProductByName(string ProducName)
        {
            string query = "SELECT * FROM dbo.Product" +
                $" WHERE Product_Name LIKE '%{ProducName}%';";

            ExecuteQuery(query);
        }
        private static List<Product> ExecuteQuery(string query)
        {
            List<Product> products = new List<Product>();
            try
            {
                using (SqlConnection connection = new SqlConnection(DataAccessLayer.Connection_String))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    products = ReadProductFromDataReader(command.ExecuteReader());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong while reading products. {ex.Message}.");
            }

            return products;
        }
        private static List<Product> ReadProductFromDataReader(SqlDataReader reader)
        {
            List<Product> products = new List<Product>();
            if (reader == null)
            {
                return products;
            }

            if (!reader.HasRows)
            {
                Console.WriteLine("No data.");
                return products;
            }

            Console.WriteLine("{0}\t{1}\t{2}\t{3}",
                    reader.GetName(0),
                    reader.GetName(1),
                    reader.GetName(2),
                    reader.GetName(3));


            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string productName = reader.GetString(1);
                decimal price = reader.GetDecimal(2);
                int categoryId = reader.GetInt32(3);

                products.Add(new Product(id, productName, price, categoryId));

                Console.WriteLine("{0} \t{1} \t{2}\t{3}", id, productName, price, categoryId);
            }
            reader.Close();

            return products;
        }

    }
}
