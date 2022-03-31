using MyFirstProject.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstProject.Shared.Services
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetProducts();
        public Task<Product> GetProduct(Guid productId);

        public Task  AddProdcut(Product product);
        
        public Task UpdateProduct(Guid productId,Product product);

        public Task AddPromoToProduct(Guid id,double promo);
        public Task DeleteProdcut(Guid productId);  

    }
}
