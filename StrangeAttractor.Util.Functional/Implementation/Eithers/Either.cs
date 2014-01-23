using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangeAttractor.Util.Functional.Interfaces;

namespace StrangeAttractor.Util.Functional.Implementation.Eithers
{
    internal abstract class Either<TLeft, TRight> : IEither<TLeft, TRight>
    {
        public ILeftProjection<TLeft, TRight> Left { get { return new LeftProjection<TLeft, TRight>(this); } }
        public IRightProjection<TLeft, TRight> Right { get { return new RightProjection<TLeft, TRight>(this); } }

        public abstract bool IsRight { get; }
        public abstract bool IsLeft { get; }

        public abstract TResult Fold<TResult>(Func<TLeft, TResult> onLeft, Func<TRight, TResult> onRight);
    }
}
