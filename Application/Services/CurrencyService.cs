using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core.Models;
using Core.Interfaces;

namespace Application.Services
{
    public class CurrencyService : BaseService<CurrencyCreateDto, CurrencyDto, Currency>, ICurrencyService
    {
        public CurrencyService(IBaseRepository<Currency> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
