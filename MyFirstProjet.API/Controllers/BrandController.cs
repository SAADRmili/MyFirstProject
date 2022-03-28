using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstProject.Shared.Entities;
using MyFirstProject.Shared.Services;

namespace MyFirstProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService ?? throw new ArgumentNullException(nameof(brandService));
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            try
            {
                var brands = await _brandService.GetBrands();
                return Ok(brands);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrand(Guid id)
        {
            try
            {
                var brand = await _brandService.GetBrand(id);
                if(brand == null)  
                    return NotFound();
                return Ok(brand);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBrand(Brand brand)
        {
            try
            {
                var result = await _brandService.AddBrand(brand);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
           
        }
    }
}
