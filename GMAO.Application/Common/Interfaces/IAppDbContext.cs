using GMAO.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<T> SetEntity<T>() where T : BaseEntity<Guid>;
        Task StartTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> SaveAllAsync(CancellationToken cancellationToken = default!);
    }
}
