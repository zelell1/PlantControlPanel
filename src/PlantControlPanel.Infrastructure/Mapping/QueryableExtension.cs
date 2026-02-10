using System.Linq.Expressions;

namespace PlantControlPanel.Infrastructure.Mapping;

public static class QueryableExtension
{
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> source, 
        bool condition, 
        Expression<Func<T, bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
}