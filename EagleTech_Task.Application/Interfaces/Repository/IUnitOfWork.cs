using EagleTech_Task.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Application.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        Task<int> SaveAsync(CancellationToken cancellationToken);
        IBaseRepo<T> Repository<T>() where T: BaseEntity;
    }
}
