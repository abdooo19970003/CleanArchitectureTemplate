using CleanArc.Application.DTOs;
using CleanArc.Application.Repositories;
using CleanArc.Domain.Common;
using CleanArc.Application.Exceptions;
using CleanArc.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using CleanArc.Application.Filter;

namespace CleanArc.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected readonly DatabaseContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(DatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().CountAsync(cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.FindAsync(id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException($"Entity with id {id} not found.");
            }
            if (entity is Entity entityBase)
            {
                entityBase.MarkAsDeleted();
                _context.Entry(entityBase).State = EntityState.Modified;
            }
            else
            {
                _dbSet.Remove(entity);
            }
            return true;
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllFilteredAsync(QueryParameters queryParameters, CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            var totalCount = await query.CountAsync(cancellationToken);
            query = query.ApplyFilters(queryParameters.Filters).Skip(queryParameters.Skip).Take(queryParameters.Take);
            var data = await query.ToListAsync(cancellationToken);
            return (IEnumerable<T>)new RepositoryResponseModel<T>(data, totalCount);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _dbSet.AsNoTracking()
                                    .FirstOrDefaultAsync(e =>
                                        EF.Property<Guid>(e, "Id") == id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException($"Entity with id {id} not found.");
            }
            return entity;
        }

        public Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult<T?>(entity);
        }
    }
}
