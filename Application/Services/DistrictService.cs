using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core.Models;
using Core.Interfaces;

namespace Application.Services
{
    public class DistrictService : BaseService<DistrictCreateDto, DistrictDto, District>, IDistrictService
    {
        public DistrictService(IBaseRepository<District> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
