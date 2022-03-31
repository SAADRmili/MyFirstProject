using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFirstProject.Shared.Entities;
using MyFirstProject.Shared.Services;


namespace MyFirstProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService  = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var products = await  _productService.GetProducts();
                return Ok(products);    
            }
            catch (Exception ex )
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            try
            {
                var product =  await _productService.GetProduct(id);
                if (product == null)
                    return BadRequest();
                return Ok(product);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                await _productService.AddProdcut(product);

                return Ok(product);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id,Product product)
        {
            try
            {
                var checkProduct = await _productService.GetProduct(id);
                if (checkProduct == null)
                    return NotFound();

                await _productService.UpdateProduct(id, product);
                return Ok(product);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts(Guid id)
        {
            try
            {
                await _productService.DeleteProdcut(id);
                return Ok();
            }
            catch (HttpRequestException ex)
            {

                return BadRequest(ex);
            }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> AddPromoProduct(Guid id,double promo)
        {
            try
            {
               await _productService.AddPromoToProduct(id, promo);
                var product = await _productService.GetProduct(id);
                return Ok(product);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
    }
}
