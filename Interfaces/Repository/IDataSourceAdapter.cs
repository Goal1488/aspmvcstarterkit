using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FuckThisNumber.Interfaces.Repository
{
    /// <summary>
    /// Adapter for underlying data provider
    /// </summary>
    public interface IDataSourceAdapter : IDisposable
    {

        DbContext UnderlyingProvider { get; }
        /// <summary>
        /// Add entity to underlying data provider
        /// </summary>
        /// <typeparam name="TEntry">Entity type that must be a referance type</typeparam>
        /// <param name="entry">Entity to add</param>
        void AddEntry<TEntry>(TEntry entry) where TEntry : class;

        /// <summary>
        /// Add array of entities to underlying data provider
        /// </summary>
        /// <typeparam name="TEntry">Entity type that must be a referance type</typeparam>
        /// <param name="entries">Entities to add</param>    
        void AddRange<TEntry>(IEnumerable<TEntry> entries) where TEntry : class;

        /// <summary>
        /// Modify entity of underlying data provider
        /// </summary>
        /// <typeparam name="TEntry">Entity type that must be a referance type</typeparam>
        /// <param name="entry">Entity to modify</param>
        void ModifyEntry<TEntry>(TEntry entry) where TEntry : class;

        /// <summary>
        /// Remove entity from underlying data provider
        /// </summary>
        /// <typeparam name="TEntry">Entity type that must be a referance type</typeparam>
        /// <param name="entry">Entity to delete</param>
        void RemoveEntry<TEntry>(TEntry entry) where TEntry : class;

        /// <summary>
        /// Return entity of underlying data provider async
        /// </summary>
        /// <typeparam name="TEntry">Entity type that must be a referance type</typeparam>
        /// <param name="param">Parameter to search by</param>
        /// <returns>Returns entity of type TEntry</returns>
        Task<TEntry> FindAsync<TEntry>(object param) where TEntry : class;

        /// <summary>
        /// Return entity of underlying data provider
        /// </summary>
        /// <typeparam name="TEntry">Entity type that must be a referance type</typeparam>
        /// <param name="param">Parameter to search by</param>
        /// <returns>Returns entity of type TEntry</returns>
        TEntry Find<TEntry>(object param) where TEntry : class;

        /// <summary>
        ///  Return all entities of underlying data provider
        /// </summary>
        /// <typeparam name="TEntry">Entity type that must be a referance type</typeparam>
        /// <returns>Returns collection of entities of type TEntry</returns>
        IEnumerable<TEntry> Entities<TEntry>() where TEntry : class;


        //int ExecuteSqlCommand(string sql, params object[] parameters);
        /// <summary>
        /// Apply changes to underlying data provider
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Apply changes to underlying data provider asynchronously
        /// </summary>
        Task SaveChangesAsync();
    }
}