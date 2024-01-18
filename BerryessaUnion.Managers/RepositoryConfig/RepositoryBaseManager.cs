using BerryessaUnion.IServices.RepositoryConfig;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BerryessaUnion.Managers.RepositoryConfig
{
    public abstract class RepositoryBaseManager<T> : IRepositoryBaseService<T> where T : class
    {
        protected ApplicationDbContext ApiDbContext { get; set; }

        public RepositoryBaseManager(ApplicationDbContext ApiDbContext)
        {
            this.ApiDbContext = ApiDbContext;
        }
        public void Create(T entity)
        {
            this.ApiDbContext.Set<T>().Add(entity);
            this.ApiDbContext.Set<T>().Add(entity);
        }
        public T CreateEntity(T entity)
        {
            this.ApiDbContext.Set<T>().Add(entity);
            this.ApiDbContext.Set<T>().Add(entity);
            return entity;
        }


        public void CreateRange(List<T> entity)
        {
            this.ApiDbContext.Set<T>().AddRange(entity);
        }

        public void Delete(T entity)
        {
            this.ApiDbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAll(Expression<Func<T, object>> expression)
        {
            return this.ApiDbContext.Set<T>().OrderBy(expression).AsNoTracking();
        }
        public IQueryable<T> GetAll()
        {
            return this.ApiDbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.ApiDbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            this.ApiDbContext.Set<T>().Update(entity);
        }
        public T UpdateEntity(T entity)
        {
            this.ApiDbContext.Set<T>().Update(entity);
            this.ApiDbContext.Set<T>().Update(entity);
            return entity;
        }
        public void UpdateRange(List<T> entity)
        {
            this.ApiDbContext.Set<T>().UpdateRange(entity);
        }


    }
}
