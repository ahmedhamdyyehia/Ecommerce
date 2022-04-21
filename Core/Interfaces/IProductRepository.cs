using System.Collections.Generic;
using Core.Entities;

namespace Core.interfaces
{
    public interface IProductRepository
    {
        Task<product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<product>> GetProductsAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypeAsync();

    }
    
}