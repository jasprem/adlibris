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
            const string sql = "SELECT * FROM tblCatalog WHERE productId=@productId";
            var products = _connection.Query<Product>(sql, new { productId });
            return new ProductAggregate(products);
        }

        public void UpdateProduct(Product product)
        {

        }

        public void AddOrder(Order order)
        {

        }
    }
}