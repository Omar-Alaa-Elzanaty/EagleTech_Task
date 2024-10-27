using EagleTech_Task.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Application.Interfaces.Repository
{
    public interface IBaseRepo<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        void Update(T entity);
        void UpdateRange(ICollection<T> entities);
        void Delete(T entity);
        void DeleteRange(ICollection<T> entities);
        Task<T?> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Entities { get; }
    }
}
