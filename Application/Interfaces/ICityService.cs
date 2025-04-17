using Application.DTOs;
using Core.Models;

namespace Application.Interfaces
{
    public interface ICityService : IBaseService<CityCreateDto, CityDto, City>
    {
    }
}