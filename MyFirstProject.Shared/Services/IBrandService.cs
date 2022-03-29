using MyFirstProject.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstProject.Shared.Services
{
    public  interface IBrandService
    {
        public Task<IEnumerable<Brand>> GetAllBrands();
        public Task<IEnumerable<Brand>> GetBrands();

        public Task<Brand> GetBrand(Guid id);

        public Task<Brand> AddBrand(Brand brand);

        public Task<Brand> UpdateBrand(Guid id, Brand brand);

        public Task DeleteBrand(Guid id);
    }
}
