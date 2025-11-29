using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity<Guid>
    {
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default!);
        Task<IReadOnlyList<TResult>> GetAllAsync<TResult>(CancellationToken cancellationToken = default!);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default!);
        Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken = default!);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken = default!);
        Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken = default!);
        Task<IReadOnlyList<T>> FilterAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default!);
        Task<IReadOnlyList<TResult>> FilterAsync<TResult>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default!);
        Task<IReadOnlyList<T>> FilterAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, IOrderedQueryable<T>>> orderBy, bool desc, CancellationToken cancellationToken = default!);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default!);
        Task AddAsync(T entity, CancellationToken cancellationToken = default!);
        Task AddRangeAsync(params T[] entities);
        void Update(T entity);
        void Remove(T entity, CancellationToken cancellationToken = default!);
        void UpdateRange(params T[] entities);
        void RemoveRange(params T[] entities);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default!);
    }
}
