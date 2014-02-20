using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrangeAttractor.Util.Functional.Tests.Data
{
    public class Item : IEntity
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public Person Owner { get; set; }
    }
}
