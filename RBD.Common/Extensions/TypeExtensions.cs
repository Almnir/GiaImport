using System;
using System.Linq.Expressions;
using System.Reflection;
using RBD.Common.Common;

namespace RBD.Common.Extensions
{
    public static class TypeExtensions
    {
        static readonly object locker = new object();
        static readonly ICache<string, string> DescriptionsCache = new DictionaryCache<string, string>();

        public static string Description<TEntity>(Expression<Func<TEntity, object>> lambda) where TEntity : class
        {
            var prop = GetPropertyFromExpression(lambda);
            if (prop == null) return string.Empty;

            lock (locker)
            {
                var key = string.Format("{0}.{1}", typeof(TEntity).FullName, prop.Name);
                return DescriptionsCache.Get(key) ?? DescriptionsCache.Insert(key, prop.GetPropertyInfoDescription());
            }
        }

        public static string PropertyName<TEntity>(Expression<Func<TEntity, object>> lambda) where TEntity : class
        {
            var prop = GetPropertyFromExpression(lambda);
            if (prop == null) return string.Empty;
            return prop.Name;
        }

        static PropertyInfo GetPropertyFromExpression<T>(Expression<Func<T, object>> lambda)
        {
            MemberExpression exp = null;
            if (lambda.Body is UnaryExpression)
            {
                var unExp = (UnaryExpression)lambda.Body;
                if (unExp.Operand is MemberExpression)
                {
                    exp = (MemberExpression)unExp.Operand;
                }
                else return null;
            }
            else if (lambda.Body is MemberExpression)
            {
                exp = (MemberExpression)lambda.Body;
            }
            else return null;

            return (PropertyInfo)exp.Member;
        }
    }
}
