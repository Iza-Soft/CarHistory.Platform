using Microsoft.EntityFrameworkCore;
using Service.History.DataAccess.Enums;
using Service.History.DataAccess.Repository.Interface;
using Service.History.DataAccess.Specs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.History.DataAccess.Repository.Class
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {

        /// <summary>
        ///     Reference to the entity framework database context
        /// </summary>
        protected DbContext DataContext = null!;

        /// <summary>
        ///     Reference to the Entity Framework Database Set class
        /// </summary>
        private DbSet<T> _dataSet = null!;

        protected RepositoryAsync(DbContext dataContext)
        {
            DataContext = dataContext;
        }

        /// <summary>
        ///     Gets the entities from the context
        /// </summary>
        private DbSet<T> Entities => _dataSet ??= DataContext.Set<T>();

#pragma warning disable CS8603 // Possible null reference return.
        public async Task<T> GetById(Guid id) => await DataContext.Set<T>().FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.

#pragma warning disable CS8603 // Possible null reference return.
        public async Task<T> GetById(int id) => await DataContext.Set<T>().FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.


        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
#pragma warning disable CS8603 // Possible null reference return.
        public async Task<T> GetSingleBySpec(ISpecification<T> spec) => (await ListAsync(spec)).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public async Task<List<T>> ListAsync(ISpecification<T> spec)
        {
            var queryableResultWithIncludes = spec.Includes.Aggregate(DataContext.Set<T>().AsQueryable(), (current, include) => current.Include(include));

            var secondaryResult = spec.IncludeStrings.Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

            var query = secondaryResult.Where(spec.Criteria);

            if (spec.SortOn != null)
            {
                query = spec.SortDirection == SortDirection.Asc ? query.OrderBy(spec.SortOn) : query.OrderByDescending(spec.SortOn);
            }

            if (spec.Offset != null)
            {
                query = query.Skip(spec.Offset.Value);
            }

            if (spec.Size != null)
            {
                query = query.Take(spec.Size.Value);
            }


            return await query.ToListAsync();
        }


        //public async Task<PagedModel<T>> GetPaginatedListAsync(ISpecification<T> spec)
        //{
        //    var queryableResultWithIncludes = spec.Includes
        //        .Aggregate(DataContext.Set<T>().AsQueryable(),
        //            (current, include) => current.Include(include));

        //    var secondaryResult = spec.IncludeStrings
        //        .Aggregate(queryableResultWithIncludes,
        //            (current, include) => current.Include(include));

        //    var query = secondaryResult
        //        .Where(spec.Criteria);

        //    if (spec.SortOn != null)
        //    {
        //        query = spec.SortDirection == SortDirection.Asc
        //            ? query.OrderBy(spec.SortOn)
        //            : query.OrderByDescending(spec.SortOn);
        //    }

        //    var totalItemsCountTask = query.CountAsync();

        //    var paged = new PagedModel<T>();

        //    if (spec.Offset != null)
        //    {
        //        query = query.Skip(spec.Offset.Value);
        //    }

        //    if (spec.Size != null)
        //    {
        //        query = query.Take(spec.Size.Value);
        //    }

        //    paged.TotalItems = await totalItemsCountTask;
        //    paged.Items = await query.ToListAsync();

        //    return paged;
        //}

        /// <summary>
        ///     Insert an entity
        /// </summary>
        /// <param name="entity">Entity to insert</param>
        /// <exception cref="InsertFailedException">Thrown if the insert fails</exception>
        public virtual async Task<T> InsertAsync(T entity)
        {
            await Entities.AddAsync(entity);

            await DataContext.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        ///     Delete an entity
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        public virtual async Task DeleteAsync(T entity)
        {
            Entities.Remove(entity);

            await DataContext.SaveChangesAsync();
        }

        /// <summary>
        ///     Update an entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        public virtual async Task UpdateAsync(T entity)
        {
            DataContext.Entry(entity).State = EntityState.Modified;

            DataContext.Update(entity);

            await DataContext.SaveChangesAsync();

        }

        /// <summary>
        ///     Search for specific entities defined by the expression
        /// </summary>
        /// <param name="predicate">Search expression</param>
        /// <returns>List of matching entities</returns>
        public virtual async Task<IList<T>> SearchForAsync(Expression<Func<T, bool>> predicate) => await Entities.Where(predicate).ToListAsync();

        /// <summary>
        ///     Search for specific entities defined by the expression and include child entities
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        public virtual async Task<IList<T>> SearchForAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeExpressions)
        {
            IQueryable<T> query = DataContext.Set<T>();

            query = includeExpressions.Aggregate(query, (current, include) => current.Include(include));

            return await query.Where(predicate).ToListAsync();
        }

        /// <summary>
        ///     Get all entities
        /// </summary>
        /// <returns>List of all entities</returns>
        public virtual async Task<IList<T>> GetAllAsync() => await Entities.ToListAsync();

        /// <summary>
        ///     
        /// </summary>
        /// <param name="includeExpressions"></param>
        /// <returns>Returns a list of entities</returns>
        public virtual IQueryable<T> IncludeAsync(params Expression<Func<T, object>>[] includeExpressions)
        {
            //IQueryable<T> query = null;
            // foreach (var include in includeExpressions)
            //  query = Entities.Include(include);
            //
            // return query;
            includeExpressions.ToList().ForEach(x => Entities.Include(x).Load());

            return Entities;
        }

        /// <summary>
        ///     Check if any item exists based of the where statement
        /// </summary>
        /// <param name="predicate">Search expression</param>
        /// <returns></returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => await Entities.AnyAsync(predicate);

        /// <summary>
        ///     Check if any item exists based of the where statement
        /// </summary>
        /// <param name="predicate">Search expression</param>
        /// <returns></returns>
#pragma warning disable CS8603 // Possible null reference return.
        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) => await Entities.FirstOrDefaultAsync(predicate);
#pragma warning restore CS8603 // Possible null reference return.

        /// <summary>
        ///     Gets the number of elements for the specified entity
        /// </summary>
        /// <returns>int</returns>
        public virtual async Task<int> CountAsync() => await Entities.CountAsync();

        public async Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate) => await Entities.Where(predicate).CountAsync();

        public async Task<List<T>> Execute(FormattableString statement)
        {
            var result = await Entities.FromSqlInterpolated(statement).ToListAsync();

            return result;
        }
    }
}