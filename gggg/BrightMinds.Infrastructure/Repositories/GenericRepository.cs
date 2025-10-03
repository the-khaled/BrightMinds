
using BrightMinds.Core.Interfaces;
using BrightMinds.Core.Models;
using BrightMinds.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly BrightMindsContext _context;
        public GenericRepository(BrightMindsContext context)
        {
            _context = context;
        }

        public async Task Add(T item)
        {
            await _context.AddAsync(item);
        }

        public void Delete(T item)
        {
            _context.Remove(item);

        }
        public void Update(T Item)
        {
            _context.Update(Item);
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().Where(T => T.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> GetWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        protected IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), spec);
        }

        public async Task AddRange(IEnumerable<T> items)
        {
            await _context.AddRangeAsync(items);
        }

        public async void DeleteRange(IEnumerable<T> items)
        {
            _context.RemoveRange(items);  
        }
    }
}
