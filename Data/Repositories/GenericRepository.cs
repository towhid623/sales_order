using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal DataContext _context;
        internal DbSet<T> _dbSet;

        public GenericRepository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Adds an entity.
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The entity that was added</returns>
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        /// <summary>
        /// Adds a list of entity.
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>The entity that was added</returns>
        public async Task<List<T>> AddAsync(List<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return entities;
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns><see cref="Task"/></returns>
        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }


        /// <summary>
        /// Deletes a list of entity.
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns><see cref="Task"/></returns>
        public Task DeleteAsync(List<T> entities)
        {
            _dbSet.RemoveRange(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Deletes entities based on a condition.
        /// </summary>
        /// <param name="filter">The condition the entities must fulfil to be deleted</param>
        /// <returns><see cref="Task"/></returns>
        public Task DeleteManyAsync(Expression<Func<T, bool>> filter)
        {
            var entities = _dbSet.Where(filter);

            _dbSet.RemoveRange(entities);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets a collection of all entities.
        /// </summary>
        /// <returns>A collection of all entities</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Gets an entity by ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve</param>
        /// <returns>The entity object if found, otherwise null</returns>
        public async Task<T> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Gets a collection of entities based on the specified criteria.
        /// </summary>
        /// <param name="filter">The condition the entities must fulfil to be returned</param>
        /// <param name="orderBy">The function used to order the entities</param>
        /// <param name="top">The number of records to limit the results to</param>
        /// <param name="skip">The number of records to skip</param>
        /// <param name="includeProperties">Any other navigation properties to include when returning the collection</param>
        /// <returns>A collection of entities</returns>
        public async Task<IEnumerable<T>> GetManyAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? top = null,
            int? skip = null,
            params string[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties.Length > 0)
            {
                query = includeProperties.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (top.HasValue)
            {
                query = query.Take(top.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<bool> AnyAsync()
        {
            return await _dbSet.AnyAsync();
        }

        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }
    }
}
