using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Presistance.Repo
{
    internal class BaseRepo<T> : IBaseRepo<T> where T : BaseEntity
    {
        private readonly OrderMangmentDbContext _context;

        public BaseRepo(OrderMangmentDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Entities => _context.Set<T>();

        public async Task AddAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task AddRangeAsync(ICollection<T> entities)
        {
            await _context.AddRangeAsync(entities);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public void DeleteRange(ICollection<T> entities)
        {
            _context.RemoveRange(entities);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public void UpdateRange(ICollection<T> entities)
        {
            _context.UpdateRange(entities);
        }
    }
}
