using System;
using System.Text;
using DataLayer;
using FuckThisNumber.Interfaces.Repository;

namespace Business.Repository
{
    /// <summary>
    ///     Basic type for implementing usage of <see cref="IAsyncRepository" />
    /// </summary>
    public abstract class RepositoryAccessor : IDisposable
    {
        protected readonly IAsyncRepository Repository;

        protected RepositoryAccessor()
        {
            Repository = GetRepository();
        }

        protected RepositoryAccessor(IAsyncRepository repository)
        {
            Repository = repository;
        }

        public virtual void Dispose()
        {
            Repository.Dispose();
        }

        /// <summary>
        ///     Get <see cref="Repository" /> instance for <see cref="ApplicationDbContext" />
        /// </summary>
        public static Repository GetRepository(bool enableValidation = true, bool proxyCreationEabled = false,
            bool lazyLoadingEnabled = false)
        {
            var rep = new Repository(
                new DbContextAdapter<ApplicationDbContext>(
                    ApplicationDbContext.Create(enableValidation, proxyCreationEabled, lazyLoadingEnabled)));
            return rep;
        }
    }
}
