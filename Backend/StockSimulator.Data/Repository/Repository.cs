using Microsoft.EntityFrameworkCore;
using StockSimulator.Data.Context;
using StockSimulator.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace StockSimulator.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly StockContext _context;

        public Repository(StockContext stockContext)
        {
            _context = stockContext;
        }
 
        #region CRUD Operations

        public virtual void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Insert(List<TEntity> list)
        {
            foreach (var entity in list)
                _context.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public virtual void Update(List<TEntity> list)
        {
            foreach (var entity in list)
                _context.Set<TEntity>().Update(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = _context.Set<TEntity>().Find(id);
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = _context.Set<TEntity>().Where(predicate).FirstOrDefault();
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>().AsQueryable();
            return query.Where(predicate);
        }

        public virtual TEntity FindById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
            DetachAllEntities();
        }

        #endregion

        public void DetachAllEntities()
        {
            var changedEntriesCopy = _context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || 
                            e.State == EntityState.Unchanged || 
                            e.State == EntityState.Added || 
                            e.State == EntityState.Detached)
                .ToList();
            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
  
    }
}
