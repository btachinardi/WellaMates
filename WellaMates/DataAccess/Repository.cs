using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Reflection;
using BootstrapSupport;
using WellaMates.Extensions;
using PagedList;

namespace WellaMates.Code.DataAccess
{
    /// <summary>
    /// Base repository class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext Context;
        private readonly Expression<Func<T, bool>> DefaultFilters;
        private readonly Func<string, Expression<Func<T, bool>>> SearchFilterProvider;

        public Repository(DbContext context, Expression<Func<T, bool>> defaultFilters, Func<string, Expression<Func<T, bool>>> searchFilterProvider)
        {
            SearchFilterProvider = searchFilterProvider;
            DefaultFilters = defaultFilters;
            Context = context;
        }

        /// <summary>
        /// Return a paged list.
        /// </summary>
        /// <param name="filters">A lambda expression used to filter the result.</param>
        /// <param name="sorting">The sort expression, example: ColumnName desc</param>
        /// <param name="includeList">The list of related entities to load.</param>
        /// <param name="currentPageNumber">The page number to return.</param>
        /// <param name="pageSize">The page size (the maximum number of elements to return in the list).</param>
        /// <returns></returns>
        public IPagedList<T> Search(Expression<Func<T, bool>> filters, string sorting, List<string> includeList, int currentPageNumber, int pageSize)
        {
            return GetQuery(Context, filters, sorting, includeList).ToPagedList(currentPageNumber, pageSize);
        }

        /// <summary>
        /// Return a list.
        /// </summary>
        /// <param name="filters">A lambda expression used to filter the result.</param>
        /// <param name="sorting">The sort expression, example: ColumnName desc</param>
        /// <param name="includeList">The list of related entities to load.</param>
        /// <returns></returns>
        public IList<T> Search(Expression<Func<T, bool>> filters, string sorting, List<string> includeList)
        {
            return GetQuery(Context, filters, sorting, includeList).ToList();
        }

        public IPagedList<T> Search(string filter, string sorting, List<string> includeList, int currentPageNumber, int pageSize)
        {
            return GetQuery(Context, filter, sorting, includeList).ToPagedList(currentPageNumber, pageSize);
        }

        public IList<T> Search(string filter, string sorting, List<string> includeList)
        {
            return GetQuery(Context, filter, sorting, includeList).ToList();
        }

        /// <summary>
        /// Method used to build the query that will reflect the filter conditions and the sort expression.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="filters"></param>
        /// <param name="sorting"></param>
        /// <param name="includeList"></param>
        /// <returns></returns>
        protected ICollection<T> GetQuery(DbContext context, Expression<Func<T, bool>> filters, string sorting, List<string> includeList)
        {
            var dbSet = context.Set<T>();
            if (string.IsNullOrEmpty(sorting))
            {
                sorting = typeof(T).GetProperties().First().Name + " Desc";
            }

            var dbQuery = (DbQuery<T>)dbSet;
            if (includeList != null)
            {
                dbQuery = includeList.Aggregate(dbQuery, (current, include) => current.Include(include));
            }

            var query = dbQuery.Where(DefaultFilters ?? (t => true));
            query = query.Where(filters ?? (t => true));
            try
            {
                return query.OrderBy(sorting).ToList();
            }
            catch (NotSupportedException)
            {
                var rawSort = sorting.Split(' ');
                var list = query.ToList();
                if (rawSort.Count() == 1 || rawSort[1] == "Asc")
                {
                    return list.SortAscending(p => p.VisibleProperties().First(vp => vp.Name == rawSort[0]).GetValue(p));
                }
                return list.SortDescending(p => p.VisibleProperties().First(vp => vp.Name == rawSort[0]).GetValue(p));
            }
        }

        /// <summary>
        /// Method used to build the query that will reflect the filter string and the sort expression.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="filter"></param>
        /// <param name="sorting"></param>
        /// <param name="includeList"></param>
        /// <returns></returns>
        protected ICollection<T> GetQuery(DbContext context, string filter, string sorting, List<string> includeList)
        {
            return GetQuery(context, filter == null ? null : SearchFilterProvider.Invoke(filter), sorting, includeList);
        }

    }
}