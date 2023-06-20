using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProductDapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductDapper.Data
{
    public class ProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }

        public Product GetProductById(int productId)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                return connection.QuerySingleOrDefault<Product>("SELECT * FROM Products WHERE ProductID = @ProductId", new { ProductId = productId });
            }
        }

        public List<Product> GetProducts()
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                return connection.Query<Product>("SELECT * FROM Products").ToList();
            }
        }

        public void AddProduct(Product product)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                connection.Execute("INSERT INTO Products (ProductName, ProductPrice) VALUES (@ProductName, @ProductPrice)", product);
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                connection.Execute("UPDATE Products SET ProductName = @ProductName, ProductPrice = @ProductPrice WHERE ProductID = @ProductID", product);
            }
        }

        public void DeleteProduct(int productId)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                connection.Execute("DELETE FROM Products WHERE ProductID = @ProductID", new { ProductID = productId });
            }
        }
    }
}
