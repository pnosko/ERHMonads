using StrangeAttractor.Util.Functional.Tests.Data;
using StrangeAttractor.Util.Functional.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace StrangeAttractor.Util.Functional.Tests.Examples
{
    public class ServiceTest
    {
        private MockDataService _dataService;
        private MockDatabaseServiceWrapper _dataServiceWrapper;

        public ServiceTest()
        {
            this._dataService = new MockDataService();
            this._dataServiceWrapper = new MockDatabaseServiceWrapper(this._dataService);
        }
        public void CanAddPerson()
        {
            var personTry = from item in this._dataServiceWrapper.AddEntity(new Item())
                    from inventory in this._dataServiceWrapper.AddEntity(new Inventory { Items = new List<Item> { item } })
                    from person in this._dataServiceWrapper.AddEntity(new Person { Inventory = inventory })
                    select person;

            // Add new item
            this._dataServiceWrapper.AddEntity(new Item { Description = "Bag of Holding" }).AsOption()
                .Run(x => personTry.AsOption()
                    .Run(p => p.Inventory.Items.Add(x)));

            Debug.Assert(personTry.IsSuccess);
        }
    }
}
