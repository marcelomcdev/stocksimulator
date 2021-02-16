using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StockSimulator.Domain.Interfaces.Services
{
    public interface IService<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);
        void Insert(List<TEntity> entity);
        void Update(TEntity entity);
        void Update(List<TEntity> entity);
        void Delete(int id);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        TEntity FindById(int id);
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        void Commit();
        void DetachAllEntities();
    }
}
