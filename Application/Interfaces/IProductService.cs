using Application.DTOs;
using Core.Models;

namespace Application.Interfaces
{
    public interface IProductService : IBaseService<ProductCreateDto, ProductDto, Product>
    {
    }
}