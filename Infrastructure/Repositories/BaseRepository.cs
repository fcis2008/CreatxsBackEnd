using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// A generic repository implementation for performing CRUD operations on entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        /// <summary>
        /// Creates a new entity in the database.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        /// <returns>The number of state entries written to the database.</returns>
        public async Task<int> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            var entityIdProperty = typeof(T).GetProperty("Id");
            return entityIdProperty == null
                ? throw new InvalidOperationException("Entity does not have an 'Id' property.")
                : (int)entityIdProperty.GetValue(entity)!;
        }

        /// <summary>
        /// Retrieves an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The entity with the specified ID, or null if not found.</returns>
        public async Task<T> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be > 0.", nameof(id));

            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Retrieves a paginated list of all entities with optional sorting, inclusion of related properties, and tracking configuration.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve (1-based index).</param>
        /// <param name="pageSize">The number of entities to retrieve per page.</param>
        /// <param name="orderBy">An optional function to order the results.</param>
        /// <param name="includeProperties">
        /// A comma-separated list of related properties to include in the query (e.g., "Property1,Property2").
        /// </param>
        /// <param name="tracked">
        /// A boolean value indicating whether the entities should be tracked by the context. 
        /// If false, the entities are retrieved as no-tracking, which improves performance for read-only operations.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of entities 
        /// for the specified page, optionally ordered and including related properties.
        /// </returns>
        public async Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, string includeProperties, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }

            if (orderBy != null)
                query = orderBy(query);

            if (!tracked)
                query = query.AsNoTracking();

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a paginated list of entities that match the specified predicate with optional sorting, inclusion of related properties, and tracking configuration.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve (1-based index).</param>
        /// <param name="pageSize">The number of entities to retrieve per page.</param>
        /// <param name="orderBy">An optional function to order the results.</param>
        /// <param name="includeProperties">
        /// A comma-separated list of related properties to include in the query (e.g., "Property1,Property2").
        /// </param>
        /// <param name="predicate">An optional predicate to filter the entities.</param>
        /// <param name="tracked">
        /// A boolean value indicating whether the entities should be tracked by the context. 
        /// If false, the entities are retrieved as no-tracking, which improves performance for read-only operations.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of entities 
        /// for the specified page that match the predicate, optionally ordered and including related properties.
        /// </returns>
        public async Task<IEnumerable<T>> FindAsync(int pageNumber, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, string includeProperties,
            Expression<Func<T, bool>>? predicate = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            if (!tracked)
                query = query.AsNoTracking();

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Id must be > 0.", nameof(id));

            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
