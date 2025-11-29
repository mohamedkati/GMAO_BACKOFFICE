using GMAO.Application.Common.Interfaces.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Persistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbContextTransaction? _currentTransaction;
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            this._context = context;
        }
        public async Task StartTransactionAsync()
        {
            if (_currentTransaction is not null)
                return;

            _currentTransaction = await this._context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction is null)
                return;
            try
            {
                if (_context.ChangeTracker.HasChanges())
                    await _context.SaveChangesAsync();
                await _currentTransaction.CommitAsync();
            }
            finally
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction == null)
                return;
            try
            {
                await _currentTransaction.RollbackAsync();
            }
            finally
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public async Task<int> SaveAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _currentTransaction?.Dispose();
        }
    }
}
