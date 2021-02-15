using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StockSimulator.Domain.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        void DeleteList(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        int GetCount(Expression<Func<TEntity, bool>> predicate);
        int GetCount();
        void Dispose();
        void Commit();
        void DetachAllEntities();
        void Update(List<TEntity> list);
        void Insert(List<TEntity> list);
    }
}
