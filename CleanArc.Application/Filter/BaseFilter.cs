using CleanArc.Application.DTOs;
using System.Linq.Expressions;
using System.Reflection;

namespace CleanArc.Application.Filter
{
    public static class BaseFilter
    {

        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, ICollection<FilterCriteria> filters)
        {
            if (filters?.Count > 0)
            {
                return query;
            }
            foreach (var filter in filters)
            {
                var propertyName = filter.Property;
                var operatorType = filter.Operator;
                var filterValue = filter.Value;

                var propertyInfo = typeof(T).GetProperty(
                    propertyName,
                    BindingFlags.IgnoreCase |
                    BindingFlags.Public |
                    BindingFlags.Instance);

                if (propertyInfo == null) throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(T).Name}'");

                var value = Convert.ChangeType(filterValue, propertyInfo.PropertyType);
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, propertyInfo);
                var constant = Expression.Constant(value);

                var body = BuildFilterExpression(property, constant, operatorType, parameter, filterValue);
                var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
                query = query.Where(lambda);
            }

            return query;
        }

        private static Expression BuildFilterExpression(Expression property, Expression constant, string operatorType, ParameterExpression parameter, object filterValue)
        {
            return operatorType.ToLower() switch
            {
                "eq" => Expression.Equal(property, constant),
                "ne" => Expression.NotEqual(property, constant),
                "gt" => Expression.GreaterThan(property, constant),
                "lt" => Expression.LessThan(property, constant),
                "gte" => Expression.GreaterThanOrEqual(property, constant),
                "lte" => Expression.LessThanOrEqual(property, constant),
                "contains" => BuildContainsExperssion(property, filterValue, parameter),
                _ => throw new ArgumentException($"Operator '{operatorType}' is not supported")
            };
        }

        private static Expression BuildContainsExperssion(Expression property, object filterValue, ParameterExpression parameter)
        {
            if (filterValue is string stringValue)
            {
                var valueList = stringValue
                    .Split(',')
                    .Select(v => Convert.ChangeType(v, property.Type))
                    .ToList();

                var constant = Expression.Constant(valueList);
                var containsMethod = typeof(List<>)
                    .MakeGenericType(property.Type)
                    .GetMethod("Contains", new[] { property.Type });

                return Expression.Call(constant, containsMethod, property);
            }

            throw new ArgumentException($"Operator 'contains' is not supported for type '{filterValue.GetType().Name}'");

        }
    }
}
