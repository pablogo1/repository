using System.Linq;

namespace Repository.Core
{
    public static class PagingExtensions
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int pageIndex, int pageSize)
        {
            var pageOptions = new PagingOptions 
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            return queryable.Page(pageOptions);
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, PagingOptions pageOptions = null)
        {
            if(pageOptions == null) return queryable;

            return queryable
                .Skip(pageOptions.PageSize * (pageOptions.PageIndex - 1))
                .Take(pageOptions.PageSize);
        }
    }
}
