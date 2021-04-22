using Order.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Order.Application.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync(List<Expression<Func<T, object>>> includes = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate = null,
            List<Expression<Func<T, object>>> includes = null);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}