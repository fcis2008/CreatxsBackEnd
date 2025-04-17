using Application.DTOs;
using Core.Models;

namespace Application.Interfaces
{
    public interface ICurrencyService : IBaseService<CurrencyCreateDto, CurrencyDto, Currency>
    {
    }
}