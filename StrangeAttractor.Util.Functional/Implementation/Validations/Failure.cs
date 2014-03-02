using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Implementation.Validations
{
    internal class Failure<TError, TValue> : IValidation<TError, TValue>
    {
        private readonly TError _exception;

        public Failure(TError exception)
        {
            this._exception = exception;
        }

        public TValue Value { get { throw new NotImplementedException(); } }

        public bool IsSuccess { get { return false; } }
        public bool IsFailure { get { return true; } }

        internal TError Error
        {
            get { return this._exception; }
        }

        public IOption<TValue> ToOption()
        {
            return Option.Nothing<TValue>();
        }

        public TResult Fold<TResult>(Func<TError, TResult> onError, Func<TValue, TResult> onSuccess)
        {
            return onError(this.Error);
        }

        public IValidation<TError, TResult> Select<TResult>(Func<TValue, TResult> selector)
        {
            return GetFailure<TResult>();
        }

        public IFailProjection<TError, TValue> Fail { get { return new FailProjection<TError, TValue>(this); } }

        //public IValidation<TError, TValue> SelectMany<TResult>(Func<T, ITry<TResult>> selector)
        //{
        //    return GetFailure<TResult>();
        //}

        //public IValidation<TError, TValue> SelectMany<TIntermediate, TResult>(Func<T, ITry<TIntermediate>> intermediate, Func<T, TIntermediate, TResult> selector)
        //{
        //    return GetFailure<TResult>();
        //}

        public IValidation<TError, TValue> Where(Func<TValue, bool> predicate)
        {
            return this;
        }

        private IValidation<TError, TResult> GetFailure<TResult>()
        {
            return Validation.Failure<TError, TResult>(this.Error);
        }
    }
}
