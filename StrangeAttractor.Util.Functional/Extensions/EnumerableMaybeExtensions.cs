using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Implementation;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Extensions
{
	public static class EnumerableOptionExtensions
	{
		public static IEnumerable<T> SelectValid<T>(this IEnumerable<IOption<T>> self)
		{
			return SelectValid(self, m => m);
		}

		public static IEnumerable<TResult> SelectValid<T, TResult>(this IEnumerable<IOption<T>> self, Func<T, TResult> selector)
		{
			return from Option in self where Option.HasValue select selector(Option.Value);
		}

		/// <summary>
		/// Determines if any Option in the collection does not encapsulate a value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static bool AnyNothing<T>(this IEnumerable<IOption<T>> self)
		{
			return self.Any(m => !m.HasValue);
		}

		/// <summary>
		/// Determines if all Option in the collection encapsulate a value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static bool All<T>(this IEnumerable<IOption<T>> self)
		{
			return self.All(m => m.HasValue);
		}

		/// <summary>
		/// Determines if any Option in the collection encapsulates a value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static bool Any<T>(this IEnumerable<IOption<T>> self)
		{
			return self.Any(m => m.HasValue);
		}

		/// <summary>
		/// Returns an encapsulated collection of values, if all values exist, otherwise returns Nothing.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static IOption<IEnumerable<T>> Sequence<T>(this IEnumerable<IOption<T>> self)
		{
			var result = self.ToLookup(x => x.HasValue, x => x.GetOrDefault());
			return result[false].Any() ? Option.Nothing<IEnumerable<T>>() : result[true].ToOption();
		}

		/// <summary>
		/// Returns an encapsulated value from the dictionary corresponding to the provided key, or Nothing if the dictionary contains no such key or the value is null.
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static IOption<TValue> GetOption<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key)
		{
			TValue value;
			return self.TryGetValue(key, out value) ? value.ToOption() : Option.Nothing<TValue>();
		}

		/// <summary>
		/// Returns the encapsulated first element of the collection, if it isn't empty, otherwise Nothing.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static IOption<T> FirstOption<T>(this IEnumerable<T> self)
		{
			return self.FirstOrDefault().ToOption();
		}

		/// <summary>
		/// Returns the encapsulated first element of the collection that satisfies the predicate, if it isn't empty, otherwise Nothing.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static IOption<T> FirstOption<T>(this IEnumerable<T> self, Func<T, bool> predicate)
		{
			return self.FirstOrDefault(predicate).ToOption();
		}

		/// <summary>
		/// Returns the encapsulated only element of the collection, otherwise Nothing.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static IOption<T> SingleOption<T>(this IEnumerable<T> self)
		{
			return self.SingleOrDefault().ToOption();
		}

		/// <summary>
		/// Returns the encapsulated only element of the collection that satisfies the predicate, otherwise Nothing.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static IOption<T> SingleOption<T>(this IEnumerable<T> self, Func<T, bool> predicate)
		{
			return self.SingleOrDefault(predicate).ToOption();
		}

		/// <summary>
		/// Returns the encapsulated only element of the collection that satisfies the predicate, otherwise Nothing.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static IOption<T> SingleOption<T>(this IQueryable<T> self, Expression<Func<T, bool>> predicate)
		{
			return self.SingleOrDefault(predicate).ToOption();
		}

		/// <summary>
		/// Returns the encapsulated only element of the collection that satisfies the predicate, otherwise Nothing.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static IOption<T> SingleOption<T>(this IQueryable<T> self)
		{
			return self.SingleOrDefault().ToOption();
		}

		/// <summary>
		/// Returns the encapsulated first element of the collection, if it isn't empty, otherwise Nothing.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static IOption<T> FirstOption<T>(this IQueryable<T> self)
		{
			return self.FirstOrDefault().ToOption();
		}

		/// <summary>
		/// Returns the encapsulated first element of the collection that satisfies the predicate, if it isn't empty, otherwise Nothing.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static IOption<T> FirstOption<T>(this IQueryable<T> self, Expression<Func<T, bool>> predicate)
		{
			return self.FirstOrDefault(predicate).ToOption();
		}
	}
}
