using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Implementation.Validations
{
    internal struct Failure<TError, TValue> : IValidation<TError, TValue>, IEquatable<IValidation<TError, TValue>>
    {
        private readonly TError _error;

        public Failure(TError error)
        {
            this._error = error;
        }

        public bool IsSuccess { get { return false; } }
        public bool IsFailure { get { return true; } }
        public bool HasValue { get { return IsSuccess; } }

        public TValue Value { get { throw new InvalidOperationException("Contains no value"); } }

        public IValidation<TValue, TError> Swapped()
        {
            return Validation.Success<TValue, TError>(this.Error);
        }

        public IOption<TValue> AsOption()
        {
            return Option.Nothing<TValue>();
        }

        public TResult Fold<TResult>(Func<TError, TResult> onError, Func<TValue, TResult> onSuccess)
        {
            return onError(this.Error);
        }

        public IValidation<TError, TResultValue> Select<TResultValue>(Func<TValue, TResultValue> selector)
        {
            return Validation.Failure<TError, TResultValue>(this.Error);
        }

        public bool Equals(IValidation<TError, TValue> other)
        {
            throw new NotImplementedException();
        }

        public TError Error { get { return this._error; } }
    }
}
