using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ProductosPractica.Models;

namespace ProductosPractica.Data
{
    public class ProductSqlDataAccessLayer
    {
        // Realizar la conexión hacia la BD, es decir, el connection string
        string connectionString = "Data Source = DESKTOP-HVK5POA; Initial Catalog=Products; user ID=grupo; Password=12345";

        // Método para obtener todos los productos
        public IEnumerable<Product> GetAllProducts()
        {
            List<Product> lst = new List<Product>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllProducts", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product();

                    product.ProductId = Convert.ToInt32(reader["ProductId"]);
                    product.ProductName = reader["ProductName"].ToString();
                    product.Category = reader["Category"].ToString();
                    product.Price = Convert.ToDecimal(reader["Price"]);
                    product.StockQuantity = Convert.ToInt32(reader["StockQuantity"]);

                    lst.Add(product);
                }

                con.Close();
            }

            return lst;
        }

        // Método para insertar un nuevo producto
        public void AddProduct(Product product)
        {

            product.Price = Convert.ToDecimal(product.Price.ToString().Replace(",", "."));

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("InsertProduct", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@Category", product.Category);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    throw new Exception("Ocurrió un error al intentar insertar el producto: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        // Método para actualizar un producto existente
        public void UpdateProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateProduct", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@Category", product.Category);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Método para eliminar un producto
        public void DeleteProduct(int? productId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteProduct", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", productId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public bool ProductExistsByName(string productName)
        {
            bool exists = false;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Products WHERE ProductName = @ProductName", con);
                cmd.Parameters.AddWithValue("@ProductName", productName);

                con.Open();
                int count = (int)cmd.ExecuteScalar();

                exists = count > 0;
                con.Close();
            }

            return exists;
        }




        // Nuevo método para obtener un producto por nombre
        public Product GetProductByName(string productName)
        {
            Product product = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetProductByName", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductName", productName);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    product = new Product
                    {
                        ProductId = Convert.ToInt32(rdr["ProductId"]),
                        ProductName = rdr["ProductName"].ToString(),
                        Category = rdr["Category"].ToString(),
                        Price = Convert.ToDecimal(rdr["Price"]),
                        StockQuantity = Convert.ToInt32(rdr["StockQuantity"])
                    };
                }

                con.Close();
            }

            return product;
        }


        // Nuevo método para eliminar un producto por nombre
        public void DeleteProductByName(string productName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteProductByName", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductName", productName);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Método para obtener un producto por su ID
        public Product GetProductById(int? productId)
        {
            Product product = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetProductById", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", productId);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    product = new Product
                    {
                        ProductId = Convert.ToInt32(rdr["ProductId"]),
                        ProductName = rdr["ProductName"].ToString(),
                        Category = rdr["Category"].ToString(),
                        Price = Convert.ToDecimal(rdr["Price"]),
                        StockQuantity = Convert.ToInt32(rdr["StockQuantity"])
                    };
                }

                con.Close();
            }

            return product;
        }
    }
}

