using System;
using System.Collections.Generic;
using RBD.Common.Enums;

namespace RBD
{
	public static class MonadicExtensions
	{
        /// <summary>
        /// c.With(c=>c.Contacts).With(c=>c.Phones)
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="o"></param>
        /// <param name="evaluator"></param>
        /// <returns></returns>
		public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
			where TResult : class 
			where TInput : class
		{
			return o == null ? null : evaluator(o);
		}

		public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue)
			where TInput : class
		{
			return o == null ? failureValue : evaluator(o);
		}

		public static TInput If<TInput>(this TInput o, Func<TInput, bool> evaluator) where TInput : class
		{
			return o == null ? null : (evaluator(o) ? o : null);
		}

		public static TInput Unless<TInput>(this TInput o, Func<TInput, bool> evaluator) where TInput : class
		{
			return o == null ? null : (evaluator(o) ? null : o);
		}

		public static TInput Do<TInput>(this TInput o, Action<TInput> action) where TInput : class
		{
			if (o == null) 
				return null;
			action(o);
			return o;
		}

		public static void Do<TInput>(this IEnumerable<TInput> o, Action<TInput> action) //where TInput : class
		{
			if (o == null) return;
		    var enumerator = o.GetEnumerator();
            while (enumerator.MoveNext())
		    {
                action(enumerator.Current);
		    }
            //foreach (var input in o)
            //{
            //    action(input);
            //}
		}

        public static string AsString(this DeleteType type)
        {
            return type != DeleteType.OK ? "Да" : "Нет";
        }

        public static string AsString(this bool type)
        {
            return type ? "Да" : "Нет";
        }
	}
}
