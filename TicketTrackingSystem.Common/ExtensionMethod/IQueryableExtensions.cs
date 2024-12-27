using System.Linq.Expressions;

namespace TicketTrackingSystem.Common.ExtensionMethod;
public static class IQueryableExtensions
{
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string propertyName, bool ascending)
    {
        var parameter = Expression.Parameter(typeof(T), "p");
        var property = Expression.PropertyOrField(parameter, propertyName);
        var lambda = Expression.Lambda(property, parameter);

        string methodName = ascending ? "OrderBy" : "OrderByDescending";
        var resultExpression = Expression.Call(
            typeof(Queryable),
            methodName,
            new Type[] { query.ElementType, property.Type },
            query.Expression,
            Expression.Quote(lambda));

        return query.Provider.CreateQuery<T>(resultExpression);
    }
}