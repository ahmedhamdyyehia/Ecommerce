using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specification;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>>ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T>Spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T>Spec);
        Task<int> CountAsync(ISpecification<T>spec);
        
    }
}