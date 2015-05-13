using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessExamples.Core.Tests
{
    /// <summary>
    ///   Test double for Entity Framework Db Sets. See https://msdn.microsoft.com/en-us/data/dn314431.aspx
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class StubDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>
            where TEntity : class
    {
        private List<TEntity> data;
        private IQueryable query;

        public StubDbSet()
        {
            data = new List<TEntity>();
            query = data.AsQueryable();
        }

        public StubDbSet(params TEntity[] initialData) : this()
        {
            AddRange(initialData);
        }

        public override TEntity Add(TEntity item)
        {
            data.Add(item);
            return item;
        }

        public override IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            var newEntities = entities.ToList();
            data.AddRange(newEntities);
            return newEntities;
        }

        public override TEntity Remove(TEntity item)
        {
            data.Remove(item);
            return item;
        }

        public override TEntity Attach(TEntity item)
        {
            data.Add(item);
            return item;
        }

        public override TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public override ObservableCollection<TEntity> Local
        {
            get { return new ObservableCollection<TEntity>(data); }
        }

        Type IQueryable.ElementType
        {
            get { return query.ElementType; }
        }

        Expression IQueryable.Expression
        {
            get { return query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return data.GetEnumerator();
        }
    } 
}
