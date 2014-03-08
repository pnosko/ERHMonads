using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Singletons;
using StrangeAttractor.Util.Functional.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StrangeAttractor.Util.Functional.Tests.Basic
{
    public class BasicTryTest
    {
        public void WhenOperationThrows_ItHasNoValue()
        {
            var result = Try.Invoke(() => GetDummy(true));

            Assert.False(result.IsSuccess);
            Assert.True(result.IsFailure);
        }

        [Fact]
        public void WhenUsingIntermediateSelectMany_ItHasAValue()
        {
            var result = from d1 in TryGetDummy()
                         from d2 in TryGetDummy()
                         select d1 == d2;

            Assert.True(result.IsSuccess);
            Assert.False(result.IsFailure);
            Assert.False(result.Value);
        }

        [Fact]
        public void WhenCallingSelectOnFailureInvariant_ItHasNoValue()
        {
            var result = TryGetDummy(true).Select(x => "new string");

            Assert.True(result.IsFailure);
            Assert.False(result.AsOption().HasValue);
        }

        [Fact]
        public void WhenRecoveringFromAFailure_ItHasAValue()
        {
            var result = TryGetDummy(true).Recover((DummyUpperException x) => new Dummy { Name = x.Message });

            Assert.True(result.IsSuccess);
            Assert.True(result.Value.Name.StartsWith("UPPER"));
        }

        [Fact]
        public void WhenRecoveringFromAFailureContravariant_ItHasAValue()
        {
            var result = TryGetLowerDummy(true).Recover((DummyUpperException x) => new Dummy { Name = x.Message });

            Assert.True(result.IsSuccess);
            Assert.True(result.Value.Name.StartsWith("UPPER: LOWER"));
        }

        [Fact]
        public void WhenRecoveringFromAFailureIncompatible_ItHasNoValue()
        {
            // incompatible argument type
            var result = TryGetLowerDummy(true).RecoverWith((ArgumentNullException x) => Try.Invoke(() => new Dummy { Name = x.Message }));

            Assert.True(result.IsFailure);
            Assert.False(result.AsOption().HasValue);
            Assert.Equal("UPPER: LOWER: lower exception", result.AsFailed().Value.Message);
        }

        [Fact]
        public void WhenRecoveringWithTryFromAFailure_ItHasAValue()
        {
            var result = TryGetDummy(true).RecoverWith((DummyUpperException x) => Try.Invoke(() => new Dummy { Name = x.Message }));

            Assert.True(result.IsSuccess);
            Assert.True(result.Value.Name.StartsWith("UPPER"));
        }

        [Fact]
        public void WhenRecoveringWithTryFromAFailureContravariant_ItHasAValue()
        {
            var result = TryGetLowerDummy(true).Recover((DummyUpperException x) => new Dummy { Name = x.Message });

            Assert.True(result.IsSuccess);
            Assert.True(result.Value.Name.StartsWith("UPPER: LOWER"));
        }

        [Fact]
        public void WhenRecoveringWithTryFromAFailureIncompatible_ItHasNoValue()
        {
            // incompatible argument type
            var result = TryGetLowerDummy(true).RecoverWith((ArgumentNullException x) => Try.Invoke(() => new Dummy { Name = x.Message }));

            Assert.True(result.IsFailure);
            Assert.False(result.AsOption().HasValue);
            Assert.Equal("UPPER: LOWER: lower exception", result.AsFailed().Value.Message);
        }

        private ITry<Dummy> TryGetDummy(bool fail = false)
        {
            return Try.Invoke(() => GetDummy(fail));
        }

        private ITry<Dummy> TryGetLowerDummy(bool fail = false)
        {
            return Try.Invoke(() => GetLowerDummy(fail));
        }

        public Dummy GetDummy(bool fail = false)
        {
            if (fail)
                throw new DummyUpperException("Exception");
            return new Dummy();
        }

        public LowerDummy GetLowerDummy(bool fail = false)
        {
            if (fail)
                throw new DummyLowerException("lower exception");
            return new LowerDummy();
        }
    }
}
