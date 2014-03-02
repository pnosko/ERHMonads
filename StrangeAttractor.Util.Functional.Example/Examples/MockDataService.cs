using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using StrangeAttractor.Util.Functional.Tests.Data;
using StrangeAttractor.Util.Functional.Extensions;
using System.ComponentModel.Design;
using System.Threading;

namespace StrangeAttractor.Util.Functional.Tests.Examples
{
    public class MockDataService : IDataService
    {
        private ServiceContainer container = new ServiceContainer();
        private Dictionary<long, object> objects = new Dictionary<long, object>();
        private long lastId = 0;

        public bool IsFaulty { get; set; }

        public IQueryable<TEntity> GetSet<TEntity>() where TEntity : IEntity
        {
            return GetList<TEntity>().AsQueryable();
        }

        public object GetObjectById(long id)
        {
            return objects.GetOption(id).GetOrNull();
        }

        public void SaveChanges(object obj)
        {
            if (IsFaulty)
                throw new InvalidOperationException("Can't save now");
        }

        public T Add<T>(T entity) where T : IEntity
        {
            var key = typeof(IList<T>);
            var svc = GetList<T>();

            if (svc.IsEmpty())
            {
                this.container.RemoveService(key);
            }
            entity.Id = Interlocked.Increment(ref lastId);

            svc.Add(entity);
            objects.Add(entity.Id, entity);
            this.container.AddService(key, svc);
            return entity;
        }

        private IList<T> GetList<T>() where T : IEntity
        {
            return this.container.GetService(typeof(IList<T>)).Cast<IList<T>>().GetOrEmpty();
        }
    }
}
