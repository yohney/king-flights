using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace System.Linq.Expressions
{
    public static class LambdaExtensions
    {
        public static void SetPropertyValue<TModel, TProperty>(this TModel target, Expression<Func<TModel, TProperty>> memberLamda, object value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null)
                {
                    property.SetValue(target, value, null);
                }
            }
        }
    }

    public static class TypeExtensions
    {
        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static Type GetUnderlyingTypeForNullable(this Type type)
        {
            if (!type.IsNullable())
                return null;

            return type.GetGenericArguments().FirstOrDefault();
        }
    }
}