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
    }
}
