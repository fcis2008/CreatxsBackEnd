using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core.Models;
using Core.Interfaces;

namespace Application.Services
{
    public class ProductService : BaseService<ProductCreateDto, ProductDto, Product>, IProductService
    {
        public ProductService(IBaseRepository<Product> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
