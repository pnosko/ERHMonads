using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StrangeAttractor.Util.Functional.Tests.Data;

namespace StrangeAttractor.Util.Functional.Tests.Examples
{
    public interface IDataService
    {
        IQueryable<TEntity> GetSet<TEntity>() where TEntity : IEntity;
        object GetObjectById(long id);

        void SaveChanges(object obj);

        T Add<T>(T entity) where T : IEntity;
    }
}
