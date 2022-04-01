using Microsoft.Extensions.Configuration;
using MyFirstProject.API.Contexts;
using MyFirstProject.API.Services;
using MyFirstProject.Shared.Services;
using System.IO;
using Xunit;

namespace MyFirstProject.Test
{
    public class ProductServiceTest
    {
        private IProductService productService;
        private IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        private dbContext dbContext;
        public ProductServiceTest()
        {
            
            dbContext = new dbContext(configuration);
            productService = new ProductService(dbContext);
        }
        [Fact]
        public async void GetAllProductsTest()
        {
            //arrange 

            var products = await productService.GetProducts();

            //act 

            //assert 
            Assert.NotNull(products);
        }
    }
}