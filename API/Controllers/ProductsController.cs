using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly StoreContext _Context;
        public ProductsController(StoreContext context)
        {
            this._Context = context;
            
        }
        [HttpGet]
        public async Task<ActionResult<List<product>>> getproducts()
        {
            var products=await _Context.products.ToListAsync ();

            return Ok(products);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<product>> getproduct( int id)
        {
            return await _Context.products.FindAsync(id);
        } 

    }
}