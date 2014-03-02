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
    public class BasicValidationTest
    {
        [Fact]
        public void WhenFailureSelectManySimple_HasExceptionValue()
        {
            var obj = GetDummy(true);

            var result = obj.SelectMany(x => Validation.Success<DummyLowerException, LowerDummy>(new LowerDummy { Name = "different" }));

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.True(result.Fail.Value.Message.EndsWith("exception"));
        }

        [Fact]
        public void WhenFailureSelectManyContravariant_HasExceptionValue()
        {
            var obj = GetDummy(true);

            var result = obj.SelectMany(x => Validation.Success<DummyUpperException, LowerDummy>(new LowerDummy { Name = "different" }));

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
            Assert.True(result.Fail.Value.Message.EndsWith("exception"));
        }

        [Fact]
        public void WhenSuccessSelectManyContravariant_HasSuccessValue()
        {
            var obj = GetDummy();

            var result = obj.SelectMany(x => Validation.Success<DummyUpperException, Dummy>(new LowerDummy { Name = "different " + x.Name}));

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
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

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.True(result.Value);
        }

        //[Fact]
        //public void WhenFailSelectManyIntermediateSimple_HasSuccessValue()
        //{
        //    var obj = GetDummy(true);
        //    var obj2 = GetDummy(true);

        //    var result = from sth in obj.Fail
        //                 from sth1 in obj2.Fail
        //                 select sth.Message == sth1.Message;

        //    Assert.True(result.IsFailure);
        //    Assert.True(result.Value);
        //}

        private IValidation<DummyLowerException, LowerDummy> GetDummy(bool fail = false)
        {
            if (fail)
            {
                return Validation.Failure<DummyLowerException, LowerDummy>(new DummyLowerException("exception"));
            }
            return Validation.Success<DummyLowerException, LowerDummy>(new LowerDummy { Name = "name" });
        }
    }
}
