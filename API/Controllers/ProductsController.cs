using Core.Entities;
using Core.interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly IProductRepository _Repo;
        public ProductsController(IProductRepository Repo)
        {
            this._Repo = Repo;
            
        }
        [HttpGet]
        public async Task<ActionResult<List<product>>> getproducts()
        {
            var products=await _Repo.GetProductsAsync();
            return Ok(products);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<product>> getproduct( int id)
        {
            return await _Repo.GetProductByIdAsync(id);
        } 
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok( await _Repo.GetProductBrandAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok( await _Repo.GetProductTypeAsync());
        }

    }
}