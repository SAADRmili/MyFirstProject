using Dapper;
using MyFirstProject.API.Contexts;
using MyFirstProject.Shared.Entities;
using MyFirstProject.Shared.Services;

namespace MyFirstProject.API.Services
{
    public class BrandService : IBrandService
    {
        private readonly dbContext _dbContext;

        public BrandService(dbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Brand> AddBrand(Brand brand)
        {
            if(brand == null)
                throw new ArgumentNullException(nameof(brand));
            var qurey = "insert into brands (id,name) values (@Id,@Name)";

            var parms = new DynamicParameters();
            parms.Add("Id",brand.Id,System.Data.DbType.Guid);
            parms.Add("Name",brand.Name,System.Data.DbType.String);

            using (var connection= _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(qurey, parms);
                return brand;
            }
            
        }

        public async Task DeleteBrand(Guid id)

        {
            var qurey = "Delete from brands WHERE   id = @Id";
            var parms = new DynamicParameters();
            parms.Add("Id", id, System.Data.DbType.Guid);

            using(var connection= _dbContext.CreateConnection())
            {
              await  connection.ExecuteAsync(qurey, parms); 
            }
      }

        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            var query = "Select * from brands";
            using(var connection= _dbContext.CreateConnection())
            {
                var brands = await connection.QueryAsync<Brand>(query);
                return brands;
            }
        }

        public async Task<Brand> GetBrand(Guid id)
        {
            var query = $"SELECT * FROM brands WHERE id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, System.Data.DbType.Guid);
            using (var connection = _dbContext.CreateConnection())
            {
                var brand = await connection.QueryFirstOrDefaultAsync<Brand>(query, parameters);
                return brand;
            }
        }

        public async Task<IEnumerable<Brand>> GetBrands()
        {
            var query = "SELECT * FROM brands b join products p on b.id = p.brandid";
            using (var connection = _dbContext.CreateConnection())
            {
                var brandDis = new Dictionary<Guid, Brand>();
                var brands = await connection.QueryAsync<Brand, Product, Brand>(
                    query, (brand, Product) =>
                    {
                        if (!brandDis.TryGetValue(brand.Id, out var currentBrand))
                        {
                            currentBrand = brand;
                            brandDis.Add(currentBrand.Id, currentBrand);
                        }
                        currentBrand.Products.Add(Product);
                        return currentBrand;
                    }
                );
                return brands.Distinct().ToList();

            }
        }

        public async Task<Brand> UpdateBrand(Guid id,Brand brand)
        {
            var query = "Update brands set name =@Name where id =@Id ";


            var parms = new DynamicParameters();
            parms.Add("Id", id, System.Data.DbType.Guid);
            parms.Add("Name", brand.Name, System.Data.DbType.String);

            using(var connection = _dbContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parms);
                return brand;
            }
        }
    }
}
