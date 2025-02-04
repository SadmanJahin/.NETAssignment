using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebApi.Shared.Models;

namespace Users.DataAccess.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query,
                PageRequest queryCriteria)
        {
            if (queryCriteria.Filters != null)
            {
                foreach (var filter in queryCriteria.Filters)
                {
                    query = query.Where(GetFilterExpressions<T>(filter));
                }
            }

            if (queryCriteria.Sorts != null)
            {
                foreach (var sort in queryCriteria.Sorts)
                {
                    query = sort.IsAscending ?
                    query.OrderBy(GetSortExpression<T>(sort)) :
                    query.OrderByDescending(GetSortExpression<T>(sort));
                }
            }

            var pagination = queryCriteria.Pagination;

            if (pagination != null)
            {
                query = query.Skip(pagination.Skip)
                             .Take(pagination.Take);
            }

            return query;
        }


        public static Expression<Func<T, bool>> GetFilterExpressions<T>(Filter filter)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, filter.PropertyName);
            var value = Expression.Constant(Convert.ChangeType(filter.Value, property.Type));

            var body = Expression.Equal(property, value);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        private static Expression<Func<T, object>> GetSortExpression<T>(Sort sort)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.PropertyOrField(parameter, sort.PropertyName);
            var convertedProperty = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<T, object>>(convertedProperty, parameter);
        }
    }
}
