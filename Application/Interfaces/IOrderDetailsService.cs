using Application.DTOs;
using Core.Models;

namespace Application.Interfaces
{
    public interface IOrderDetailsService : IBaseService<OrderDetailsCreateDto, OrderDetailsDto, OrderDetail>
    {
         Task<int> CreateListAsync(List<OrderDetailsCreateDto> createDto);
    }
}