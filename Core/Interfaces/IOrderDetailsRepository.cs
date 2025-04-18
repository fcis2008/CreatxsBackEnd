using Core.Models;

namespace Core.Interfaces
{
    public interface IOrderDetailsRepository
    {
        public Task<int> CreateOrder(List<OrderDetail> orderDetail);
    }
}
