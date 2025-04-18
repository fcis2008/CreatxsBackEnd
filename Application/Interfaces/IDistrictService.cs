using Application.DTOs;
using Core.Models;

namespace Application.Interfaces
{
    public interface IDistrictService : IBaseService<DistrictCreateDto, DistrictDto, District>
    {
    }
}