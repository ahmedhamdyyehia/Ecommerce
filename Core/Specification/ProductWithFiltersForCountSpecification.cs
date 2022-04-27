using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specification
{
    public class ProductWithFiltersForCountSpecification:BaseSpecification<product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams ProductParams)
            :base(x=>
            (string.IsNullOrEmpty(ProductParams.Search) || x.Name.ToLower()
            .Contains(ProductParams.Search))&&
            (!ProductParams.BrandId.HasValue || x.ProductBrandId==ProductParams.BrandId) && 
            (!ProductParams.TypeId.HasValue||x.ProductTypeId==ProductParams.TypeId )
            )
        {
            
        }
        
    }
}