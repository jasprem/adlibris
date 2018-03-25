using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Catalog.Domain;
using Dapper;

namespace Catalog.Persistence
{
    public interface IProductRepository
    {
        ProductAggregate Get(string productId);
        void UpdateProduct(Product product);
        void AddOrder(Order order);
        ProductStatus GetProductStatus(string productId);
        void AddNewProductStatus(ProductStatus productStatus);
        void UpdateProductStatus(ProductStatus productStatus);
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
            const string sql = "UPDATE tblCatalog SET available = @available WHERE productId = @productId  AND shelf = @shelf";
            _connection.Execute(sql, new
            {
                available = product.Available,
                productId = product.ProductId,
                shelf = product.Shelf
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

        public ProductStatus GetProductStatus(string productId)
        {
            const string sql = "SELECT * FROM tblProductStatus WHERE productId = @productId";
            return _connection.Query<ProductStatus>(sql, new
            {
                productId
            }).FirstOrDefault();
        }

        public void AddNewProductStatus(ProductStatus productStatus)
        {
            const string sql = "INSERT INTO tblProductStatus (productId, totalAvailable) Values (@productId, @totalAvailable);";
            _connection.Execute(sql, new
            {
                productId = productStatus.ProductId,
                totalAvailable = productStatus.TotalAvailable
            });
        }

        public void UpdateProductStatus(ProductStatus productStatus)
        {
            const string sql = "UPDATE tblProductStatus SET totalAvailable = @totalAvailable WHERE productId = @productId";
            _connection.Execute(sql, new
            {
                productId = productStatus.ProductId,
                totalAvailable = productStatus.TotalAvailable
            });
        }
    }
}