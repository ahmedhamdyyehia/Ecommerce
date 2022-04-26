using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.interfaces;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // [ApiController]
    // [Route("api/[Controller]")]
    public class ProductsController:BaseApiController
    {
        private readonly IGenericRepository<product>_productsRepo;
        private readonly IGenericRepository<ProductBrand>_productBrandRepo;
        private readonly IGenericRepository<ProductType>_productTypeRepo;
        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<product> productsRepo
        ,IGenericRepository<ProductBrand>productBrandRepo
        ,IGenericRepository<ProductType>productTypeRepo
        ,IMapper mapper
        )
        {
            _mapper=mapper;
            _productsRepo=productsRepo;
            _productBrandRepo=productBrandRepo;
            _productTypeRepo=productTypeRepo;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> getproducts(
           [FromQuery]ProductSpecParams ProductParams)
           //string sort ,int? brandId,int? typeId)
        {
            var Spec=new ProductsWithTypesAndBrandsSpecification(ProductParams);
            var CountSpec=new ProductWithFiltersForCountSpecification(ProductParams);
            var totalItems= await _productsRepo.CountAsync(CountSpec);
            var products=await _productsRepo.ListAsync(Spec);
            var data =_mapper
                .Map<IReadOnlyList<product>,IReadOnlyList<ProductToReturnDto>>(products);

            return Ok( new Pagination<ProductToReturnDto>(ProductParams.PageIndex
            ,ProductParams.PageSize,totalItems,data));

            // return products.Select(product=>new ProductToReturnDto
            // {
            //     Id=product.Id,
            //     Name=product.Name,
            //     Description=product.Description,
            //     PictureUrl=product.PictureUrl,
            //     Price=product.Price,
            //     ProductBrand=product.ProductBrand.Name,
            //     ProductType=product.ProductType.Name

            // }).ToList();

        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> getproduct( int id)
        {
            var Spec=new ProductsWithTypesAndBrandsSpecification(id);
            var product= await _productsRepo.GetEntityWithSpec(Spec);
            if( product ==null)
            return NotFound(new ApiResponse(404));
            return _mapper.Map<product,ProductToReturnDto>(product);
            // new  ProductToReturnDto
            // {
            //     Id=product.Id,
            //     Name=product.Name,
            //     Description=product.Description,
            //     PictureUrl=product.PictureUrl,
            //     Price=product.Price,
            //     ProductBrand=product.ProductBrand.Name,
            //     ProductType=product.ProductType.Name

            // };
        } 
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok( await _productBrandRepo.ListAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok( await _productTypeRepo.ListAllAsync());
        }

    }
}