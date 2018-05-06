using CardTrend.DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CardTrend.DAL.Concrete
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class,new()
    {
        private log4net.ILog _Logger;
        protected readonly DbContext opContext;
        protected readonly IDbSet<T> dbSet;

        public BaseRepository(IDbContext opContext)
        {
            this.opContext = opContext.CurrentContext;
            this.dbSet = this.opContext.Set<T>();
        }

        //When you expect a model back (async)
        public async Task<IList<T>> ExecWithStoreProcedureAsync<T>(string query, params object[] parameters)
        {
            return await opContext.Database.SqlQuery<T>(query, parameters).ToListAsync();
        }
        //When you expect a model back
        public IEnumerable<T> ExecWithStoreProcedure<T>(string query)
        {
            return opContext.Database.SqlQuery<T>(query);
        }

        // Fire and forget (async)
        public async Task ExecuteWithStoreProcedureAsync(string query, params object[] parameters)
        {
            await opContext.Database.ExecuteSqlCommandAsync(query, parameters);
        }
        // Fire and forget
        public void ExecuteWithStoreProcedure(string query, params object[] parameters)
        {
            opContext.Database.ExecuteSqlCommand(query, parameters);
        }
        /// <summary>
        /// Get object by its key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(object id)
        {
            try
            {
                return dbSet.Find(id);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Get all item
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            try
            {

                //Logger.Info("Before getting data - Base repo");
                return dbSet.ToList();
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            try
            {
                opContext.Entry(entity).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            try
            {
                opContext.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                throw;
            }
        }
        /// <summary>
        /// Insert new entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            try
            {
                dbSet.Add(entity);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                throw;
            }
        }

        /// <summary>
        /// Get item based on specified predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return dbSet.FirstOrDefault(predicate);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                throw;
            }
        }

    }
}
