using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithTypesAndBrandsSpecification:BaseSpecification<product>
    {
        public ProductsWithTypesAndBrandsSpecification( ProductSpecParams ProductParams )
            :base(x=>
            (string.IsNullOrEmpty(ProductParams.Search) || x.Name.ToLower()
            .Contains(ProductParams.Search))&&
            (!ProductParams.BrandId.HasValue || x.ProductBrandId==ProductParams.BrandId) && 
            (!ProductParams.TypeId.HasValue||x.ProductTypeId==ProductParams.TypeId )
            )
        {
            AddInculde(x=>x.ProductBrand);
            AddInculde(x=>x.ProductType);
            AddOrderBy(x=>x.Name);
            ApplyPaging(ProductParams.PageSize*(ProductParams.PageIndex -1)
            ,ProductParams.PageSize);

            if(!string.IsNullOrEmpty(ProductParams.Sort))
            {
                switch(ProductParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p=>p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p=>p.Price);
                        break;
                    default:
                    AddOrderBy(n=>n.Name);
                        break;    
                   
                }
            }
            
        }

        public ProductsWithTypesAndBrandsSpecification(int id)
         : base(x=>x.Id==id)
        {
            AddInculde(x=>x.ProductType);
            AddInculde(x=>x.ProductBrand);
        }
    }
}