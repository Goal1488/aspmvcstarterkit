using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class DbContextAdapter<TSource> : DataSourceAdapter<TSource> where TSource : DbContext, new()
    {
        public DbContextAdapter(TSource underlyingProvider)
            : base(underlyingProvider)
        {
        }

        public DbContextAdapter(Type type)
            : base(type)
        {
        }

        public DbContextAdapter() : this(new TSource()) { }

        public override void AddEntry<TEntry>(TEntry entry)
        {
            lock (entry)
            {
                UnderlyingProvider.Entry(entry).State = EntityState.Added;
                //DeepEntryUpdate(entry);
            }
        }

        public override void AddRange<TEntry>(IEnumerable<TEntry> entries)
        {
            lock (AddRangeLocker)
            {
                UnderlyingProvider.Set<TEntry>().AddRange(entries);
            }
        }

        public override void ModifyEntry<TEntry>(TEntry entry)
        {
            lock (entry)
            {
                var existingEntity = Find<TEntry>(DbRepositoryHelper.GetEntityId(entry));
                //SetValues(entry, existingEntity);

                UnderlyingProvider.Entry(existingEntity).CurrentValues.SetValues(entry);
                UnderlyingProvider.Entry(existingEntity).State = EntityState.Modified;
                //DeepEntryUpdate(entry);
            }
        }

        public override void RemoveEntry<TEntry>(TEntry entry)
        {
            lock (entry)
            {
                UnderlyingProvider.Entry(entry).State = EntityState.Deleted;
            }
        }

        public override Task<TEntry> FindAsync<TEntry>(object param)
        {
            return UnderlyingProvider.Set<TEntry>().FindAsync(param);
        }

        public override TEntry Find<TEntry>(object param)
        {
            return UnderlyingProvider.Set<TEntry>().Find(param);
        }

        public override IEnumerable<TEntry> Entities<TEntry>()
        {
            return UnderlyingProvider.Set<TEntry>();
        }

        public override void SaveChanges()
        {
            UnderlyingProvider.SaveChanges();
        }

        public override Task SaveChangesAsync()
        {
            return UnderlyingProvider.SaveChangesAsync();
        }


        /// <summary>
        /// Copy values from one object ti another
        /// </summary>
        /// <param name="objFrom"></param>
        /// <param name="objTo"></param>
        public void SetValues(object objFrom, object objTo)
        {
            var objToType = objTo.GetType();
            var objFromType = objFrom.GetType();
            var fromProperties = objFromType
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead && p.CanWrite)
                .ToList();

            foreach (var property in objToType.GetProperties())
            {
                var fromProperty = fromProperties.FirstOrDefault(p => p.Name.ToLower() == property.Name.ToLower());
                if (fromProperty == null) continue;

                var fromValue = fromProperty.GetValue(objFrom);

                property.SetValue(objTo, fromValue);

                #region TODO

                //if (property.PropertyType.GetInterface(typeof (IEnumerable).FullName) != null)
                //{

                //}
                //else if (property.PropertyType.IsClass)
                //{

                //    property.SetValue(objTo, fromValue);
                //}
                //else
                //{
                //    property.SetValue(objTo, fromValue);
                //}

                #endregion
            }
        }

        public override void Dispose()
        {
            UnderlyingProvider.Dispose();
        }
    }
}