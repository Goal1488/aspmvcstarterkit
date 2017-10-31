using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using FuckThisNumber.Interfaces.Repository;

namespace Business.Repository
{
    public abstract class DataSourceAdapter<TSource> : IDataSourceAdapter where TSource : DbContext, new()
    {
        protected object AddRangeLocker = new object();
        public TSource UnderlyingProvider { get; }

        DbContext IDataSourceAdapter.UnderlyingProvider
        {
            get { return this.UnderlyingProvider; }
        }

        protected DataSourceAdapter(TSource underlyingProvider)
        {
            UnderlyingProvider = underlyingProvider;
        }

        protected DataSourceAdapter(Type type)
        {
            if (type != typeof(TSource))
            {
                throw new ArgumentException("Wrong type of passing argument", "type");
            }
            UnderlyingProvider = Activator.CreateInstance(type) as TSource;
        }

        protected DataSourceAdapter()
            : this(new TSource())
        {
        }

        public abstract void AddEntry<TEntry>(TEntry entry) where TEntry : class;
        public abstract void AddRange<TEntry>(IEnumerable<TEntry> entries) where TEntry : class;
        public abstract void ModifyEntry<TEntry>(TEntry entry) where TEntry : class;
        public abstract void RemoveEntry<TEntry>(TEntry entry) where TEntry : class;
        public abstract Task<TEntry> FindAsync<TEntry>(object param) where TEntry : class;
        public abstract TEntry Find<TEntry>(object param) where TEntry : class;
        public abstract IEnumerable<TEntry> Entities<TEntry>() where TEntry : class;

        public virtual void SaveChanges() { throw new NotImplementedException(); }

        public virtual Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public virtual void Dispose()
        {
            var disposable = UnderlyingProvider as IDisposable;
            if (disposable != null) disposable.Dispose();
            else throw new Exception("Underlying provider can not be disposed.");
        }
    }
}