using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using StrangeAttractor.Util.Functional.Extensions;
using StrangeAttractor.Util.Functional.Tests.Data;

namespace StrangeAttractor.Util.Functional.Tests
{
    public class BasicOptionTest
    {
        [Fact]
        public void WhenConvertingNullItDoesNotHaveValue()
        {
            Person obj = null;
            var result = obj.ToOption();

            Assert.False(result.HasValue);
        }

        [Fact]
        public void WhenCastingToInvalidTypeItDoesNotHaveValue()
        {
            Person obj = new Person();
            var result = obj.Cast<Item>();

            Assert.False(result.HasValue);
        }

        [Fact]
        public void WhenUsingIntermediateSelectManyAndNotJoinedItHasNoValue()
        {
            var result = from val1 in (new Person()).ToOption()
                         from val2 in (new Item()).ToOption()
                         select val2.Owner == val1;

            Assert.True(result.HasValue);
            Assert.False(result.GetOrDefault());
        }
    }
}
