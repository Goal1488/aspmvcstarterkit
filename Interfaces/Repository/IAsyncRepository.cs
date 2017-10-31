using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuckThisNumber.Interfaces.Repository
{
    /// <summary>
    /// Provides basic CRUD operations with underlying data provider through an adapter
    /// </summary>
    public interface IAsyncRepository : IRepository
    {
        /// <summary>
        /// Add new entity of specified type
        /// </summary>
        /// <typeparam name="TEntity">Entity type that must be a referance type</typeparam>
        /// <param name="entity">Entity to add</param>
        /// <returns>Returns operation result object with status of operation and operated entity</returns>
        Task<IOperationResult<TEntity>> AddAsync<TEntity>(TEntity entity) where TEntity : class;
        
        /// <summary>
        /// Update an existing entity of specified type
        /// </summary>
        /// <typeparam name="TEntity">Entity type that must be a referance type</typeparam>
        /// <param name="entity">Entity to update</param>
        /// <returns>Returns operation result object with status of operation and operated entity</returns>
        Task<IOperationResult<TEntity>> UpdateAsync<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Add or update an entity of specified type
        /// </summary>
        /// <typeparam name="TEntity">Entity type that must be a referance type</typeparam>
        /// <param name="entity">Entity to add or update</param>
        /// <returns>Returns operation result object with status of operation and operated entity</returns>
        Task<IOperationResult<TEntity>> AddOrUpdateAsync<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Delete entity of specified type
        /// </summary>
        /// <typeparam name="TEntity">Entity type that must be a referance type</typeparam>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Returns operation result status</returns>
        Task<EOperationResultStatus> DeleteAsync<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Return entity of specified type
        /// </summary>
        /// <typeparam name="TEntity">Entity type that must be a referance type</typeparam>
        /// <param name="param">Parameter to search by</param>
        /// <returns>Returns entity of type TEntry</returns>
        Task<TEntity> FindAsync<TEntity>(object param) where TEntity : class;

        Task<List<TEntity>> ToListAsync<TEntity>(IQueryable<TEntity> source) where TEntity : class;

        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
    }
}