using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FuckThisNumber.Interfaces.Repository
{
    public interface IEntityStore
    {
        /// <summary>
        /// Return all entities of specified type
        /// </summary>
        /// <typeparam name="TEntity">Entity type that must be a referance type</typeparam>
        /// <returns>Returns collection of entities of type TEntry</returns>
        IQueryable<TEntity> Entities<TEntity>() where TEntity : class;

        /// <summary>
        /// Apply changes to underlying provider
        /// </summary>
        /// <returns>Returns operation result status</returns>
        Task<IOperationResult> SaveChangesAsync();

        DbContext Context { get; }
    }
}