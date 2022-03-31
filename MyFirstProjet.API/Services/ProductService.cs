using Dapper;
using Microsoft.AspNetCore.Mvc;
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
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task AddProdcut(Product product)
        {
            var query = "insert into products (id,name,price,promoprice,brandid) values (@Id,@Name,@Price,@Promoprice,@BrandId)";
            var prams = new DynamicParameters();


            if(product !=null)
            {
                prams.Add("Id", Guid.NewGuid(), System.Data.DbType.Guid);
                prams.Add("Name", product.Name, System.Data.DbType.String);
                prams.Add("Price", product.Price, System.Data.DbType.Double);
                prams.Add("Promoprice", 0.0, System.Data.DbType.Double);
                prams.Add("BrandId", product.BrandId, System.Data.DbType.Guid);

                using(var connection = _dbContext.CreateConnection())
                {
                    var brandquery = "select * from brands where id = @BrandId";
                    var brand = await connection.QueryFirstOrDefaultAsync(brandquery,prams);
                    if (brand == null)
                    {
                        throw new ArgumentNullException(nameof(brand));
                    }

                    await connection.ExecuteAsync(query, prams);
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

           


        }

        public async Task AddPromoToProduct(Guid id,double promo)
        {
            var product = await GetProduct(id);
            if(product == null)
                throw new ArgumentNullException(nameof(product));


            var query = "Update products set promoprice = price - (price * @Promo) where id= @Id ";
            var parms = new DynamicParameters();
            parms.Add("Id", id, System.Data.DbType.Guid);
            parms.Add("Promo", promo, System.Data.DbType.Double);
            using( var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parms);
                
            }

        }

        public async Task DeleteProdcut(Guid productId)
        {
            var query = "Delete from products where id = @Id";
            var  parms = new DynamicParameters();
            parms.Add("Id", productId,System.Data.DbType.Guid);

            using (var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parms);
            }
        }

        public async Task<Product> GetProduct(Guid productId)
        {
            var query = "SELECT * FROM products WHERE id = @Id"  ;
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

        public async Task UpdateProduct(Guid productId,Product product)
        {
            var query = "Update products set name = @Name ,price = @Price ,brandid=@brandId where id = @Id";
            var prams = new DynamicParameters();

            prams.Add("Id", productId, System.Data.DbType.Guid);
            prams.Add("Name", product.Name, System.Data.DbType.String);
            prams.Add("Price", product.Price, System.Data.DbType.Double);
            prams.Add("Promoprice", 0.0, System.Data.DbType.Double);
            prams.Add("BrandId", product.BrandId, System.Data.DbType.Guid);

            using ( var connection = _dbContext.CreateConnection() )
            {
                await connection.ExecuteAsync(query, prams);

            }

        }
    }
}
