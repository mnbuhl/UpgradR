using Microsoft.EntityFrameworkCore;
using Order.Application.Interfaces;
using Order.Domain.Common;
using Order.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly OrderContext _context;

        public GenericRepository(OrderContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> entities = _context.Set<T>();

            orderBy?.Invoke(entities);

            return await entities.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}