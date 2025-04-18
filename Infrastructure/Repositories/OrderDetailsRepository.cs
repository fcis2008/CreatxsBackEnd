using Core.Models;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateOrder(List<OrderDetail> orderDetail)
        {
            if (orderDetail == null || orderDetail.Count == 0)
            {
                throw new ArgumentException("Order details cannot be null or empty.", nameof(orderDetail));
            }

            var order = new Order
            {
                CreatedAt = DateTime.UtcNow,
            };

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            return orderDetail.Count; // Return the number of order details created
        }
    }
}
