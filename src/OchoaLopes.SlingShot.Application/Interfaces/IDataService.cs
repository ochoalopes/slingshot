using OchoaLopes.SlingShot.Application.Dtos;
using System.Linq.Expressions;

namespace OchoaLopes.SlingShot.Application.Interfaces
{
    public interface IDataService<TDto> where TDto : BaseDto
    {
        Task<IEnumerable<TDto>> Get(Expression<Func<TDto, bool>>? filter = null, Func<IQueryable<TDto>, IOrderedQueryable<TDto>>? orderBy = null, string includeProperties = "");
        Task<TDto?> GetByIdAsync(string id);
        Task<IEnumerable<TDto>> GetAllAsync();
        Task AddAsync(TDto dto);
        void Update(TDto dto);
        void Delete(TDto dto);
    }
}
