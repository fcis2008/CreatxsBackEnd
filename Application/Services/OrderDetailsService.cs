using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core.Models;
using Core.Interfaces;

namespace Application.Services
{
    public class OrderDetailsService : BaseService<OrderDetailsCreateDto, OrderDetailsDto, OrderDetail>, IOrderDetailsService
    //public class OrderDetailsService : IOrderDetailsService
    {
        protected readonly IMapper _mapper;
        protected readonly IOrderDetailsRepository _repository;

        public OrderDetailsService(IBaseRepository<OrderDetail> repository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
        }

        public async Task<int> CreateListAsync(List<OrderDetailsCreateDto> createDto)
        {
            var orderDetailsList = _mapper.Map<List<OrderDetail>>(createDto);

            return await _repository.CreateOrder(orderDetailsList);
        }
    }
}
