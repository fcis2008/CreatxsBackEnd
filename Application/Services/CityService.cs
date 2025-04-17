using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core.Models;
using Core.Interfaces;

namespace Application.Services
{
    public class CityService : BaseService<CityCreateDto, CityDto, City>, ICityService
    {
        public CityService(IBaseRepository<City> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
