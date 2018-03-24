using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Catalog.Domain;
using Dapper;

namespace Catalog.Persistence
{
    public interface IProductRepository
    {
        ProductAggregate Get(string productId);
        void UpdateProduct(Product product);
        void AddOrder(Order order);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;


        public ProductRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        public ProductAggregate Get(string productId)
        {
            const string sql = "SELECT * FROM tblCatalog WHERE productId = @productId";
            var products = _connection.Query<Product>(sql, new
            {
                productId
            });
            return new ProductAggregate(products);
        }

        public void UpdateProduct(Product product)
        {
            const string sql = "UPDATE tblCatalog SET available = @available WHERE productId = @productId";
            _connection.Execute(sql, new
            {
                available = product.Available,
                productId = product.ProductId
            });
        }

        public void AddOrder(Order order)
        {
            const string sql = "INSERT INTO tblReservation (productId, customerId, shelf, totalOrdered) Values (@productId, @customerId, @shelf, @totalOrdered);";
            _connection.Execute(sql, new
            {
                productId = order.ProductId,
                customerId = order.CustomerId,
                shelf = order.Shelf,
                totalOrdered = order.TotalOrdered
            });
        }
    }
}