using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrangeAttractor.Util.Functional.Tests.Data
{
    public class Inventory : IEntity
    {
        public long Id { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
