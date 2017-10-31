using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FuckThisNumber.Interfaces.Repository;

namespace Business.Repository
{
    public class Repository : IAsyncRepository
    {
        public Repository(IDataSourceAdapter adapter)
        {
            if (adapter == null)
                throw new ArgumentNullException("adapter");

            Adapter = adapter;

        }
        
        protected virtual IDataSourceAdapter Adapter { get; private set; }

        public Task<IOperationResult<TEntity>> AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            IOperationResult<TEntity> result;

            try
            {
                Adapter.AddEntry(entity);

                result = new OperationResult<TEntity>(EOperationResultStatus.EntryAdded, entity);
            }
            catch (Exception e)
            {
                result = new OperationResult<TEntity>(e);
            }

            

            return Task.FromResult(result);
        }
        
        public Task<IOperationResult<TEntity>> UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            IOperationResult<TEntity> result;

            try
            {
                Adapter.ModifyEntry(entity);
                result = new OperationResult<TEntity>(EOperationResultStatus.EntryUpdated, entity);
            }
            catch (Exception e)
            {
                result = new OperationResult<TEntity>(e);
            }

            return Task.FromResult(result);
        }

        public virtual async Task<IOperationResult<TEntity>> AddOrUpdateAsync<TEntity>(TEntity entity)
            where TEntity : class
        {

            IOperationResult<TEntity> result;

            result = DbRepositoryHelper.IsNewEntry(entity)
                ? await AddAsync(entity)
                : await UpdateAsync(entity);

            if (result.Status == EOperationResultStatus.WrongOperation)
                return result;

            try
            {
                await Adapter.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result = new OperationResult<TEntity>(ex);
            }

            return result;
        }

        public virtual Task<EOperationResultStatus> DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                Type typeParameterType = typeof(TEntity);
                var isDeleted = typeParameterType.GetInterfaces().Any(t => t.Name == "IDeleted");
                if (isDeleted)
                {
                    PropertyInfo propIsDeleted = typeParameterType.GetProperty("IsDeleted");
                    propIsDeleted.SetValue(entity, true, null);
                }
                else
                {
                    Adapter.RemoveEntry(entity);
                }
            }
            catch (Exception)
            {
                return Task.FromResult(EOperationResultStatus.WrongOperation);
            }
            return Task.FromResult(EOperationResultStatus.EntryDeleted);
        }

        public virtual Task<TEntity> FindAsync<TEntity>(object param) where TEntity : class
        {
            return Adapter.FindAsync<TEntity>(param);
        }

        public Task<List<TEntity>> ToListAsync<TEntity>(IQueryable<TEntity> source) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TEntity> Entities<TEntity>() where TEntity : class
        {
            return Adapter.Entities<TEntity>().AsQueryable();
        }

        public virtual async Task<IOperationResult> SaveChangesAsync()
        {
            IOperationResult result;

            try
            {
                await Adapter.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result = new OperationResult(ex);
            }

            result = new OperationResult(EOperationResultStatus.DataSaved);

            return result;
        }

        public DbContext Context => this.Context;

        public void Dispose()
        {
            Adapter.Dispose();
        }

        public IOperationResult Add<TEntity>(TEntity entity) where TEntity : class
        {
            IOperationResult result;

            try
            {
                Adapter.AddEntry(entity);
                result = new OperationResult<TEntity>(EOperationResultStatus.EntryAdded, entity);
            }
            catch (Exception e)
            {
                result = new  OperationResult<TEntity>(e);
            }


            return result;
        }

        public IOperationResult AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            IOperationResult result;

            try
            {
                Adapter.AddRange(entities);

                result = new OperationResult(EOperationResultStatus.EntryAdded);
            }
            catch (Exception e)
            {
                result = new OperationResult(e);
            }

            return result;
        }

        public IOperationResult Update<TEntity>(TEntity entity) where TEntity : class
        {
            IOperationResult result;

            try
            {
                Adapter.ModifyEntry(entity);

                result = new OperationResult<TEntity>(EOperationResultStatus.EntryUpdated, entity);
            }
            catch (Exception e)
            {
                result = new OperationResult<TEntity>(e);
            }

            return result;
        }

        public IOperationResult AddOrUpdate<TEntity>(TEntity entity) where TEntity : class
        {
            IOperationResult result = DbRepositoryHelper.IsNewEntry(entity)
                ? Add(entity)
                : Update(entity);

            if (result.Status == EOperationResultStatus.WrongOperation)
                return result;

            try
            {
                Adapter.SaveChanges();
            }
            catch (Exception ex)
            {
                result = new OperationResult(ex);
            }

            return result;
        }

        public EOperationResultStatus Delete<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                Type typeParameterType = typeof(TEntity);
                var isDeleted = typeParameterType.GetInterfaces().Any(t => t.Name == "IDeleted");
                if (isDeleted)
                {
                    PropertyInfo propIsDeleted = typeParameterType.GetProperty("IsDeleted");
                    propIsDeleted.SetValue(entity, true, null);
                }
                else
                {
                    Adapter.RemoveEntry(entity);
                }
            }
            catch (Exception)
            {
                return EOperationResultStatus.WrongOperation;
            }

            return EOperationResultStatus.EntryDeleted;
        }

        public TEntity Find<TEntity>(object param) where TEntity : class
        {
            return Adapter.Find<TEntity>(param);
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return Adapter.UnderlyingProvider.Database.ExecuteSqlCommand(sql, parameters);
        }

        public DbRawSqlQuery<TEntity> SqlQuery<TEntity>(string sql, params object[] parameters)
        {
            return Adapter.UnderlyingProvider.Database.SqlQuery<TEntity>(sql, parameters);
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return Adapter.UnderlyingProvider.Database.ExecuteSqlCommandAsync(sql, parameters);
        }
    }
}