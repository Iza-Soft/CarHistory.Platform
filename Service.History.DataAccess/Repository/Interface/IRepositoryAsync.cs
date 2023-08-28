using Service.History.DataAccess.Specs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.History.DataAccess.Repository.Interface
{
    public interface IRepositoryAsync<T> where T : class
    {
        /// <summary>
        ///     Get item by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetById(Guid id);

        /// <summary>
        ///     Get item by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<T> GetSingleBySpec(ISpecification<T> spec);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<List<T>> ListAsync(ISpecification<T> spec);

        //Task<PagedModel<T>> GetPaginatedListAsync(ISpecification<T> spec);

        /// <summary>
        /// Insert an entity
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        Task<T> InsertAsync(T entity);

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Search for specific entities defined by the expression
        /// </summary>
        /// <param name="predicate">Search expression</param>
        /// <returns>List of matching entities</returns>
        Task<IList<T>> SearchForAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>List of all entities</returns>
        Task<IList<T>> GetAllAsync();

        /// <summary>
        /// Creates a generic any
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> where);

        /// <summary>
        ///     Check if any item exists based of the where statement
        /// </summary>
        /// <param name="predicate">Search expression</param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets the number of elements for the specified entity 
        /// </summary>
        /// <returns>int</returns>
        Task<int> CountAsync();

        /// <summary>
        /// Gets the number of elements for the specified entity 
        /// </summary>
        /// <returns>int</returns>
        Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>> Execute(FormattableString statement);
    }
}