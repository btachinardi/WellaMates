using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using PagedList;

namespace WellaMates.Code.DataAccess
{
    /// <summary>
    /// Base repository class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Return a paged list.
        /// </summary>
        /// <param name="filters">A lambda expression used to filter the result.</param>
        /// <param name="sorting">The sort expression, example: ColumnName desc</param>
        /// <param name="includeList">The list of related entities to load.</param>
        /// <param name="currentPageNumber">The page number to return.</param>
        /// <param name="pageSize">The page size (the maximum number of elements to return in the list).</param>
        /// <returns></returns>
        IPagedList<T> Search(Expression<Func<T, bool>> filters, string sorting, List<string> includeList, int currentPageNumber, int pageSize);
 
        /// <summary>
        /// Return a list.
        /// </summary>
        /// <param name="filters">A lambda expression used to filter the result.</param>
        /// <param name="sorting">The sort expression, example: ColumnName desc</param>
        /// <param name="includeList">The list of related entities to load.</param>
        /// <returns></returns>
        IList<T> Search(Expression<Func<T, bool>> filters, string sorting, List<string> includeList);

        /// <summary>
        /// Return a paged list.
        /// </summary>
        /// <param name="filter">A string to be compared to every field of the entity.</param>
        /// <param name="sorting">The sort expression, example: ColumnName desc</param>
        /// <param name="includeList">The list of related entities to load.</param>
        /// <param name="currentPageNumber">The page number to return.</param>
        /// <param name="pageSize">The page size (the maximum number of elements to return in the list).</param>
        /// <returns></returns>
        IPagedList<T> Search(string filter, string sorting, List<string> includeList, int currentPageNumber, int pageSize);

        /// <summary>
        /// Return a list.
        /// </summary>
        /// <param name="filter">A string to be compared to every field of the entity.</param>
        /// <param name="sorting">The sort expression, example: ColumnName desc</param>
        /// <param name="includeList">The list of related entities to load.</param>
        /// <returns></returns>
        IList<T> Search(string filter, string sorting, List<string> includeList);
    }
}