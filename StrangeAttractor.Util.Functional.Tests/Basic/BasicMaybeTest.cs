using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using StrangeAttractor.Util.Functional.Extensions;

namespace StrangeAttractor.Util.Functional.Tests
{
    public class BasicOptionTest
    {
        private class Dummy
        {
        }

        private class SeparateDummy
        {
            public Dummy Dummy { get; set; }
        }

        [Fact]
        public void WhenConvertingNullItDoesNotHaveValue()
        {
            Dummy obj = null;
            var result = obj.ToOption();

            Assert.False(result.HasValue);
        }

        [Fact]
        public void WhenCastingToInvalidTypeItDoesNotHaveValue()
        {
            var obj = new Dummy();
            var result = obj.Cast<SeparateDummy>();

            Assert.False(result.HasValue);
        }

        [Fact]
        public void WhenUsingIntermediateSelectManyAndNotJoinedItHasNoValue()
        {
            var dummy = new Dummy();
            var other = new SeparateDummy{ Dummy = dummy };

            var result = from val1 in dummy.ToOption()
                         from val2 in other.ToOption()
                         select val2.Dummy == val1;

            Assert.True(result.HasValue);
            Assert.True(result.GetOrDefault());
        }

        [Fact]
        public void WhenUsingIntermediateSelectManyAndNotJoinedItHasNoValue()
        {
            var result = from val1 in (new Dummy()).ToOption()
                         from val2 in ((SeparateDummy)null).ToOption()
                         select val2.Dummy == val1;

            Assert.False(result.HasValue);
        }
    }
}
