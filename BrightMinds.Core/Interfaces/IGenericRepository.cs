using BrightMinds.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightMinds.Core.Interfaces
{
    public interface IGenericRepository<T>where T : BaseEntity
    {

        public  Task Add(T item);
        public Task AddRange(IEnumerable<T> items);
        public void DeleteRange(IEnumerable<T> items);  
        public void Delete(T item);

        public void Update(T Item);
        public Task<T> GetAsync(int id);
        public Task<IReadOnlyList<T>> GetAllAsync();
        public Task<T> GetWithSpecAsync(ISpecification<T> spec);
        public Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        public Task<int> GetCountWithSpecAsync(ISpecification<T> spec);
        Task<T> GetByIdAsync(int coursid);
    }
}
