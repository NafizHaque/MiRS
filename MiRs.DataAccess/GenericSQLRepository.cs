using Microsoft.EntityFrameworkCore;
using MiRS.Gateway.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.DataAccess
{
    /// <summary>
    /// Class that implements IGenericSQLRepository for using Azure SQL database.
    /// </summary>
    /// <typeparam name="TEntity">Object for table.</typeparam>
    public class GenericSQLRepository<TEntity> : IGenericSQLRepository<TEntity>
        where TEntity : class
    {
        private readonly RuneHunterDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericSQLRepository(RuneHunterDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Gets a single entity based on the provided Id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <param name="cancellationToken">The Cancellation token.</param>
        /// <returns>Returns a single TEntity object.</returns>
        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(id, cancellationToken);
        }

        /// <summary>
        /// Adds an entity to a table.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="TEntity"/> Returns the object that was created.</returns>
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Adds a range of entities to a table.
        /// </summary>
        /// <param name="entities">The range of entities to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the entity in the table with the updated values in the provided entity.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an entity from a table.
        /// </summary>
        /// <param name="entity">Entity to be delted.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a range of entities from a table.
        /// </summary>
        /// <param name="entities">The range of entities to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns a set of results from a table, based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter to run against the table data.</param>
        /// <param name="continuationToken">The cancellation token.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> filter, string? continuationToken = null)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }

        /// <summary>
        /// Gets all entities from a table.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync();
        }
    }
}
