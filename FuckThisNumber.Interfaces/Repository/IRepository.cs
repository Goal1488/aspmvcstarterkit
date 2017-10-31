using System;

namespace FuckThisNumber.Interfaces.Repository
{
    /// <summary>
    /// Provides basic CRUD operations with underlying data provider through an adapter
    /// </summary>
    public interface IRepository : IEntityStore, IDisposable
    {
        /// <summary>
        /// Add new entity of specified type
        /// </summary>
        /// <typeparam name="TEntity">Entity type that must be a referance type</typeparam>
        /// <param name="entity">Entity to add</param>
        /// <returns>Returns operation result object with status of operation and operated entity</returns>
        IOperationResult Add<TEntity>(TEntity entity) where TEntity : class;
        
        /// <summary>
        /// Update an existing entity of specified type
        /// </summary>
        /// <typeparam name="TEntity">Entity type that must be a referance type</typeparam>
        /// <param name="entity">Entity to update</param>
        /// <returns>Returns operation result object with status of operation and operated entity</returns>
        IOperationResult Update<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Add or update an entity of specified type
        /// </summary>
        /// <typeparam name="TEntity">Entity type that must be a referance type</typeparam>
        /// <param name="entity">Entity to add or update</param>
        /// <returns>Returns operation result object with status of operation and operated entity</returns>
        IOperationResult AddOrUpdate<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Delete entity of specified type
        /// </summary>
        /// <typeparam name="TEntity">Entity type that must be a referance type</typeparam>
        /// <param name="entity">Entity to delete</param>
        /// <returns>Returns operation result status</returns>
        EOperationResultStatus Delete<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Return entity of specified type
        /// </summary>
        /// <typeparam name="TEntity">Entity type that must be a referance type</typeparam>
        /// <param name="param">Parameter to search by</param>
        /// <returns>Returns entity of type TEntry</returns>
        TEntity Find<TEntity>(object param) where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, params object[] parameters);
    }
}