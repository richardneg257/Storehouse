using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Context;
using WebApi.Dtos;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ProductsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get()
        {
            var products = await context.Products.ToListAsync();
            return products;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if(product == null) return NotFound();

            return product;
        }

        [HttpPost]
        public async Task Post([FromBody] ProductCreationDto productCreationDto)
        {
            var productEntity = new Product()
            {
                Code = productCreationDto.Code,
                Name = productCreationDto.Name,
                Description = productCreationDto.Description,
                Stock = productCreationDto.Stock,
                CreationDate = DateTime.Now
            };

            context.Products.Add(productEntity);
            await context.SaveChangesAsync();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(int id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            var entity = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if(entity == null) return NotFound($"No se encontró el Producto con id = {id}");

            entity.Code = productUpdateDto.Code;
            entity.Name = productUpdateDto.Name;
            entity.Description = productUpdateDto.Description;
            entity.Stock = productUpdateDto.Stock;

            context.Products.Update(entity);
            await context.SaveChangesAsync();

            return Ok(entity);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) return NotFound();

            context.Products.Remove(entity);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
