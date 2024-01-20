using API_asp_start_project.Domain.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace API_asp_start_project.Domain.Helpers
{
    public class SortHelper<T> : ISortHelper<T>
    {
        public IQueryable<T>? ApplySort(IQueryable<T> entities, string orderByQueryString)
        {
            if (!entities.Any())
            {
                return entities;
            }

            if (string.IsNullOrEmpty(orderByQueryString))
            {
                return entities;
            }

            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryExpressions = new List<Expression<Func<T, object>>>();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrEmpty(param))
                {
                    continue;
                }

                var propertyFromQueryName = param.Split(" ")[0];
                PropertyInfo? objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty != null)
                {
                    continue;
                }

                var parameter = Expression.Parameter(typeof(T), "x");
                var propertyAccess = Expression.Property(parameter, objectProperty);
                var orderByExpression = Expression.Lambda<Func<T, object>>(propertyAccess, parameter);

                orderQueryExpressions.Append(orderByExpression);
            }

            if (orderQueryExpressions.Count == 0)
            {
                return entities;
            }

            IOrderedQueryable<T>? orderedQuery = null;
            foreach (var order in orderQueryExpressions)
            {
                if (orderedQuery == null)
                {
                    orderedQuery = entities.OrderBy(order);
                }
                else
                {
                    orderedQuery = orderedQuery.ThenBy(order);
                }
            }

            return orderedQuery;
        }
    }
}
