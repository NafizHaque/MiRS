using System.Linq.Expressions;

namespace MiRS.Gateway.DataAccess
{
    public interface IGenericSQLRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets a single entity based on the provided Id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <param name="cancellationToken">The Cancellation token.</param>
        /// <returns>Returns a single TEntity object.</returns>
        Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds an entity to a table.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="TEntity"/> Returns the object that was created.</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a range of entities to a table.
        /// </summary>
        /// <param name="entities">The range of entities to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the entity in table storage with the updated values in the provided entity using the merge UpdateMode.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an entity from a table.
        /// </summary>
        /// <param name="entity">Entity to be delted.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a range of entities from a table.
        /// </summary>
        /// <param name="entities">The range of entities to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a set of results from a table, based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter to run against the table data.</param>
        /// <param name="continuationToken">The cancellation token.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IEnumerable<TEntity>> Query(Expression<Func<TEntity, bool>> filter, string? continuationToken = null);

        /// <summary>
        /// Gets all entities from a table.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        Task<IEnumerable<TEntity>> GetAllEntitiesAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Adds an entity to a table with Insert Identity.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddWithIdentityInsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
