using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstProject.API.DTO;
using MyFirstProject.Shared.Entities;
using MyFirstProject.Shared.Services;
using System.Collections.Generic;

namespace MyFirstProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;
        public BrandController(IBrandService brandService, IMapper mapper)
        {
            _brandService = brandService ?? throw new ArgumentNullException(nameof(brandService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));    
        }


        [HttpGet("/GetAllBrands",Name ="GetAllBrands")]
        public async Task<IActionResult> AllBrands()
        {
            try
            {
                var brands = await _brandService.GetAllBrands();
                var data = _mapper.Map<IEnumerable<BrandDetails>>(brands);
                return Ok(data);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpGet(Name ="GetBrandsWithOurProducts")]
        public async Task<IActionResult> GetBrandsWithProduct()
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


        [HttpPut("{id}")]
        public  async Task<IActionResult> UpdateBrand(Guid id , Brand brandUpdated)

        {
            try
            {
                var brand = await _brandService.GetBrand(id);
                if (brand == null)
                   return NotFound();

              var  brandUp = await _brandService.UpdateBrand(id,brandUpdated) ;
                return Ok(brandUp);

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public  async Task<IActionResult> DeleteBrand(Guid id)
        {
            try
            {
                var brand = await _brandService.GetBrand(id);
                if (brand == null)
                    return NotFound();

                await  _brandService.DeleteBrand(id);
                return Ok();
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
