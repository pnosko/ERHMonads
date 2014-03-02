using System;
using StrangeAttractor.Util.Functional.Extensions;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Implementation.Validations
{
    internal class FailProjection<TError, TValue> : IFailProjection<TError, TValue>
    {
        private readonly IValidation<TError, TValue> _validation;

        public FailProjection(IValidation<TError, TValue> validation)
        {
            this._validation = validation;
        }

        public IValidation<TResultError, TValue> Select<TResultError>(Func<TError, TResultError> selector)
        {
            return this._validation.Fold(
                e => Validation.Failure<TResultError, TValue>(selector(e)),
                s => Validation.Success<TResultError, TValue>(s));
        }

        public TError Value
        {
            get { return this.ToOption().GetOrThrow(() => new InvalidOperationException("No error value")); }
        }

        public IOption<TError> ToOption()
        {
            return this._validation.Fold(
                e => e.ToOption(),
                s => Option.Nothing<TError>());
        }

        public TResult Fold<TResult>(Func<TError, TResult> onFailure, Func<TValue, TResult> onSuccess)
        {
            return this._validation.Fold(onFailure, onSuccess);
        }
    }
}
