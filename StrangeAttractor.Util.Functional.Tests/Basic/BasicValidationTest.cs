using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Singletons;
using StrangeAttractor.Util.Functional.Extensions;
using Xunit;

namespace StrangeAttractor.Util.Functional.Tests.Basic
{
    public class BasicDisjunctionTest
    {
        [Fact]
        public void WhenFailureSelectManySimple_HasExceptionValue()
        {
            var obj = GetDummy(true);

            var result = obj.SelectMany(x => Disjunction.Right<DummyLowerException, LowerDummy>(new LowerDummy { Name = "different" }));

            Assert.False(result.IsRight);
            Assert.True(result.IsLeft);
            Assert.True(result.Swapped().Value.Message.EndsWith("exception"));
        }

        [Fact]
        public void WhenFailureSelectManyContravariant_HasExceptionValue()
        {
            var obj = GetDummy(true);

            var result = obj.SelectMany(x => Disjunction.Right<DummyUpperException, LowerDummy>(new LowerDummy { Name = "different" }));

            Assert.False(result.IsRight);
            Assert.True(result.IsLeft);
            Assert.True(result.Swapped().Value.Message.EndsWith("exception"));
        }

        [Fact]
        public void WhenSuccessSelectManyContravariant_HasSuccessValue()
        {
            var obj = GetDummy();

            var result = obj.SelectMany(x => Disjunction.Right<DummyUpperException, Dummy>(new LowerDummy { Name = "different " + x.Name }));

            Assert.True(result.IsRight);
            Assert.False(result.IsLeft);
            Assert.True(result.Value.Name.Equals("different name"));
        }

        [Fact]
        public void WhenSuccessSelectManyIntermediateSimple_HasSuccessValue()
        {
            var obj = GetDummy();
            var obj2 = GetDummy();

            var result = from sth in obj
                         from sth1 in obj2
                         select sth.Name == sth1.Name;

            Assert.True(result.IsRight);
            Assert.False(result.IsLeft);
            Assert.True(result.Value);
        }

        private IDisjunction<DummyLowerException, LowerDummy> GetDummy(bool fail = false)
        {
            if (fail)
            {
                return Disjunction.Left<DummyLowerException, LowerDummy>(new DummyLowerException("exception"));
            }
            return Disjunction.Right<DummyLowerException, LowerDummy>(new LowerDummy { Name = "name" });
        }
    }
}
