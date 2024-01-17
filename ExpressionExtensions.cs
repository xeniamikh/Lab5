using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BooksCatalogUI
{
    public static class ExpressionExtensions
    {
        private const string ArgumentIsNotAPropertyException = "Argument is not a property";
        private const string InvalidArgumentException = "Invalid argument";

        public static string GetPropertyName<T>(this Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var propertyInfo = propertyExpression.GetPropertyInfo();
            if (propertyInfo == null)
            {
                throw new ArgumentException(ArgumentIsNotAPropertyException, "propertyExpression");
            }

            return propertyInfo.Name;
        }

        public static string GetPropertyName<T, TP>(this Expression<Func<T, TP>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var pi = propertyExpression.GetPropertyInfo();
            if (pi == null)
            {
                throw new ArgumentException(ArgumentIsNotAPropertyException, "propertyExpression");
            }

            return pi.Name;
        }

        public static PropertyInfo GetPropertyInfo<T, TP>(this Expression<Func<T, TP>> propertyExpression)
        {
             if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            return GetPropertyInfoCore(propertyExpression.Body);
        }

        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            return GetPropertyInfoCore(propertyExpression.Body);
        }

        private static PropertyInfo GetPropertyInfoCore(Expression propertyExpression)
        {
            var memberExpression = propertyExpression as MemberExpression;

            var unaryExpression = propertyExpression as UnaryExpression;
            if (unaryExpression != null)
            {
                memberExpression = unaryExpression.Operand as MemberExpression;
            }

            if (memberExpression != null)
            {
                return memberExpression.Member as PropertyInfo;
            }

            throw new ArgumentException(InvalidArgumentException, "propertyExpression");
        }
    }
}
