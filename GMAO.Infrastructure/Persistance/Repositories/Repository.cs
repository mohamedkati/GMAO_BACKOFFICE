using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity<Guid>
    {
        protected readonly AppDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly DbSet<T> _entityTable;

        public Repository(AppDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
            this._entityTable = context.Set<T>();
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken = default!)
        {
            if (predicate == null)
                return await _entityTable.AnyAsync(cancellationToken);

            return await _entityTable.AnyAsync(predicate, cancellationToken);
        }

        public async Task<IReadOnlyList<T>> FilterAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default!)
        {
            return await _entityTable.Where(predicate).AsNoTracking().ToListAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<TResult>> FilterAsync<TResult>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default!)
        {
            return await _entityTable
                .Where(predicate)
                .AsNoTracking()
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<T>> FilterAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, IOrderedQueryable<T>>> orderBy, bool desc, CancellationToken cancellationToken = default!)
        {
            if (desc)
                return await _entityTable.Where(predicate).AsNoTracking().OrderByDescending(orderBy).ToListAsync(cancellationToken);
            else
                return await _entityTable.Where(predicate).AsNoTracking().OrderBy(orderBy).ToListAsync(cancellationToken);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken = default!)
        {
            if (predicate == null)
                return await _entityTable.FirstOrDefaultAsync(cancellationToken);

            return await _entityTable.FirstOrDefaultAsync(predicate, cancellationToken);
        }
        public async Task<TResult?> FirstOrDefaultAsync<TResult>(Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken = default!)
        {
            if (predicate == null)
                return await _entityTable
                    .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);

            return await _entityTable
                .Where(predicate)
                 .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default!)
        {
            return await _entityTable.AsNoTracking().ToListAsync(cancellationToken);
        }
        public async Task<IReadOnlyList<TResult>> GetAllAsync<TResult>(CancellationToken cancellationToken = default!)
        {
            return await _entityTable
                .AsNoTracking()
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default!)
        {
            return await _entityTable.FindAsync(id, cancellationToken);
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default!)
        {
            if (predicate == null)
                return await _entityTable.CountAsync(cancellationToken);

            return await _entityTable.CountAsync(predicate, cancellationToken);
        }
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default!)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _entityTable.AddAsync(entity, cancellationToken);
        }
        public async Task AddRangeAsync(params T[] entities)
        {
            await _entityTable.AddRangeAsync(entities);
        }
        public void Update(T entity)
        {
            _entityTable.Update(entity);
        }
        public void Remove(T entity, CancellationToken cancellationToken = default!)
        {
            _entityTable.Remove(entity);
        }
        public void UpdateRange(params T[] entities)
        {
            foreach (var entity in entities)
                _entityTable.Update(entity);
        }
        public void  RemoveRange(params T[] entities)
        {
            _entityTable.RemoveRange(entities);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default!)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
