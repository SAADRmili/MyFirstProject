using Dapper;
using MyFirstProject.API.Contexts;
using MyFirstProject.Shared.Entities;
using MyFirstProject.Shared.Services;

namespace MyFirstProject.API.Services
{
    public class ProductService : IProductService
    {
        private readonly dbContext _dbContext; 
        
        public ProductService(dbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)) ;
        }

        public void AddProdcut(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProduct(Guid productId)
        {
            var query = $"SELECT * FROM products WHERE id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", productId, System.Data.DbType.Guid);
            using (var connection = _dbContext.CreateConnection())
            {
                var product = await connection.QueryFirstOrDefaultAsync<Product>(query,parameters);
                return product;
            }
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var query = "SELECT * FROM products";
            using(var connection = _dbContext.CreateConnection() )
            {
                var products = await connection.QueryAsync<Product>(query);
                return products.ToList();
            }
        }
    }
}
