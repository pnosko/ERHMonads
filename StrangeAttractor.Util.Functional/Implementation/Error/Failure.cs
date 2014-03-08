using System;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Singletons;
using StrangeAttractor.Util.Functional.Extensions;

namespace StrangeAttractor.Util.Functional.Implementation.Error
{
    internal struct Failure<T> : ITry<T>, IEquatable<ITry<T>>
    {
        private readonly Exception _error;

        public Failure(Exception exception)
        {
            this._error = exception;
        }

        public bool IsSuccess { get { return false; } }
        public bool IsFailure { get { return true; } }
        public bool HasValue { get { return IsSuccess; } }

        public T Value { get { throw this.Error; } }

        internal Exception Error { get { return this._error; } }

        public IOption<T> AsOption()
        {
            return Option.Nothing<T>();
        }

        public TResult Fold<TResult>(Func<Exception, TResult> onError, Func<T, TResult> onSuccess)
        {
            return onError(this.Error);
        }

        public ITry<TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return GetFailure<TResult>();
        }

        public ITry<TResult> SelectMany<TResult>(Func<T, ITry<TResult>> selector)
        {
            return GetFailure<TResult>();
        }

        public ITry<TResult> SelectMany<TIntermediate, TResult>(Func<T, ITry<TIntermediate>> intermediate, Func<T, TIntermediate, TResult> selector)
        {
            return GetFailure<TResult>();
        }

        public ITry<T> Where(Func<T, bool> predicate)
        {
            return this;
        }

        public IOption<Exception> AsFailed()
        {
            return this.Error.ToOption();
        }

        private ITry<TResult> GetFailure<TResult>()
        {
            return Try.Failure<TResult>(this.Error);
        }

        public bool Equals(ITry<T> other)
        {
            var err = this.Error;
            return other.IsFailure && other.AsFailed().SelectOrDefault(x => x.Equals(err));
        }
    }
}
