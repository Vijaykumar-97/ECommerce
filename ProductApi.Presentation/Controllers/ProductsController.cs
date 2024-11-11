using eCommerce.SharedLibrary.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application;
using ProductApi.Application.DTO_s;
using ProductApi.Application.DTO_s.Conversions;
using ProductApi.Domain.Entities;

namespace ProductApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProduct productInterface) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            //get all products from repo
            var products = await productInterface.GetAllAsync();

            if (!products.Any())
                return NotFound("No Products detected in the database");

            //convert data from entity to DTO and return
            var (_, list) = ProductConversion.FromEntity(null!, products);
            return list!.Any() ? Ok(list) : NotFound("No Product found");

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            //Get single product from repo
            var product = await productInterface.FincByIdAsync(id);

            if (product is null)
                return NotFound("Product requested is not found");

            //convert data from entity to DTO and return
            var (_Product, _) = ProductConversion.FromEntity(product, null!);
            return _Product is not null ? Ok(_Product) : NotFound("Product not found");

        }

        [HttpPost]

        public async Task<ActionResult<Response>> CreateProduct(ProductDTO product)
        {
            // check Model state is all data annotations are passed

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            //convert to DTO to entity and pass to repo
            var getEntity = ProductConversion.ToEntity(product); 
            var response = await productInterface.CreateAsync(getEntity);
            return response.Flag is true ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<ActionResult<Response>> UpdateProduct(ProductDTO product)
        {
            // check Model state is all data annotations are passed

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //convert to DTO to entity and pass to repo
            var getEntity = ProductConversion.ToEntity(product);
            var response = await productInterface.UpdateAsync(getEntity);
            return response.Flag is true ? Ok(response) : BadRequest(response);

        }

        [HttpDelete]
        public async Task<ActionResult<Response>> DeleteProduct(ProductDTO product)
        {
            //convert to DTO to entity and pass to repo
            var getEntity = ProductConversion.ToEntity(product);
            var response = await productInterface.DeleteAsync(getEntity);
            return response.Flag is true ? Ok(response) : BadRequest(response);
        }
    }
}
