using EagleTech_Task.Application.Interfaces.Repository;
using EagleTech_Task.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Presistance.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderMangmentDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(OrderMangmentDbContext context)
        {
            _context = context;
            _repositories = [];
        }


        public IBaseRepo<T> Repository<T>() where T : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepo<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IBaseRepo<T>)_repositories[type]!;
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync();
        }
    }
}
