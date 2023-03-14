using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryCategoryExtensions
    {
        public static IQueryable<Category> Sort(this IQueryable<Category> categories, string OrderByQueryString)
        {
            if (string.IsNullOrEmpty(OrderByQueryString))
                return categories.OrderBy(e => e.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Category>(OrderByQueryString);

            if (string.IsNullOrEmpty(orderQuery))
                return categories.OrderBy(e => e.Name);

            return categories.OrderBy(orderQuery);
        }

        public static IQueryable<Category> Search(this IQueryable<Category> categories, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return categories;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return categories.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Category> CustomInclude(this IQueryable<Category> categories, string includes)
        {
            if (string.IsNullOrWhiteSpace(includes)) return categories;

            var IncludesArray = includes.Split(",");

            foreach (var include in IncludesArray)
            {
               categories = categories.Include(include);
            }

            return categories;
        }
    }
}
