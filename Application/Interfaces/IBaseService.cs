namespace Application.Interfaces
{
    public interface IBaseService<TCreateDto, TDto, TEntity>
    {
        Task<int> CreateAsync(TCreateDto createDto);
        Task<TDto> GetByIdAsync(int id);
        Task<IEnumerable<TDto>> GetAllAsync(int pageNumber, int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties, bool tracked = true);
        Task UpdateAsync(int id, TDto updateDto);
        Task DeleteAsync(int id);
    }
}