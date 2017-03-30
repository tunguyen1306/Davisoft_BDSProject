using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Davisoft_BDSProject.Domain.Helpers
{
    public static class QueryHelper
    {
        /// <summary>
        /// Source: http://www.thomaslevesque.com/2010/10/03/entity-framework-using-include-with-lambda-expressions/
        /// Modified by Dang H. to support including using anonymouse type
        /// </summary>
        public static IQueryable<T> Include<T>(this IQueryable<T> source, params Expression<Func<T, object>>[] selectors) where T : class
        {
            Debug.Print("----- EAGER LOAD DETAILS -----");
            return selectors.SelectMany(selector => new PropertyPathVisitor().GetPropertyPaths(selector))
                            .Aggregate(source, (query, path) =>
                                               {
                                                   Debug.Print(path);
                                                   return query.Include(path);
                                               });
        }

        private class PropertyPathVisitor : ExpressionVisitor
        {
            private readonly List<Stack<string>> _list = new List<Stack<string>>();

            public IEnumerable<string> GetPropertyPaths(Expression expression)
            {
                GetPropertiesPath(expression);

                return _list.Aggregate(new List<string>(),
                                       (list, stack) =>
                                       {
                                           list.Add(stack.Aggregate(new StringBuilder(),
                                                                    (sb, name) => sb.Append(sb.Length > 0 ? "." : "")
                                                                                    .Append(name)).ToString());
                                           return list;
                                       });
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                if (!_list.Any())
                    _list.Add(new Stack<string>());

                foreach (Stack<string> stack in _list)
                    stack.Push(node.Member.Name);

                return base.VisitMember(node);
            }

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                if (IsLinqOperator(node.Method))
                {
                    for (int i = 1; i < node.Arguments.Count; i++)
                        Visit(node.Arguments[i]);

                    Visit(node.Arguments[0]);

                    return node;
                }

                return base.VisitMethodCall(node);
            }

            protected override Expression VisitNew(NewExpression node)
            {
                foreach (Expression property in node.Arguments)
                    _list.AddRange(new PropertyPathVisitor().GetPropertiesPath(property));

                return node;
            }

            private IEnumerable<Stack<string>> GetPropertiesPath(Expression expression)
            {
                Visit(expression);
                return _list;
            }

            private static bool IsLinqOperator(MethodInfo method)
            {
                if (method.DeclaringType != typeof (Queryable) &&
                    method.DeclaringType != typeof (Enumerable))
                    return false;

                return Attribute.GetCustomAttribute(method, typeof (ExtensionAttribute)) != null;
            }
        }
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string SortField, bool Ascending)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = (MemberExpression)null;
            var exp = (LambdaExpression)null;
            if (SortField.IndexOf(".") > -1)
            {

                var parts = SortField.Split('.');

                Expression parent = param;

                foreach (var part in parts)
                {
                    parent = Expression.Property(parent, part);
                }

                exp = Expression.Lambda(parent, param);
            }
            else
            {
                prop = Expression.Property(param, SortField);
                exp = Expression.Lambda(prop, param);
            }
            string method = Ascending ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { q.ElementType, exp.Body.Type };
            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(mce);
        }
        public static string RawQuery(this IQueryable query)
        {
            // Source: http://stackoverflow.com/a/1412902
            return ((ObjectQuery) query).ToTraceString();
        }
    }
}
