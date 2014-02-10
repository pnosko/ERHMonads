using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Extensions;
using StrangeAttractor.Util.Functional.Singletons;
using StrangeAttractor.Util.Functional.Tests.Data;

namespace StrangeAttractor.Util.Functional.Tests.Examples
{
    public class MockDatabaseServiceWrapper
    {
        private readonly IDataService _service;

        public MockDatabaseServiceWrapper(IDataService service)
        {
            this._service = service;
        }

        public IOption<Person> GetPersonById(long id)
        {
            return this._service.GetSet<Person>().SingleOption(x => x.Id == id);
        }

        public IOption<Item> GetItemById(long id)
        {
            return this._service.GetObjectById(id).ToOption().Cast<Item>();
        }

        public ITry<T> AddEntity<T>(T entity) where T : IEntity
        {
            return Try.Invoke(() => this._service.Add(entity));
        }

        public ITry<Person> UpdatePerson(long id, Action<Person> update)
        {
            return this.GetPersonById(id).TrySelect(x =>
            {
                update(x);
                this._service.SaveChanges(x);
                return x;
            });
        }
    }
}
