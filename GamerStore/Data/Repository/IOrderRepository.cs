using System.Data.SqlTypes;
using GamerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GamerStore.Data.Repository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersAsync();

        Task<Order> GetOrderByIdAsync(int id);

        Task<int> SaveOrderAsync(Order order);

        Task UpdateAsync(Order order);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDbContext context;

        public OrderRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            var orders = await this.context.Orders.ToListAsync();
            return orders;
        }

        public async Task<int> SaveOrderAsync(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            this.context.AttachRange(order.Lines.Select(l => l.Product));

            if (order.OrderId != 0)
            {
                throw new SqlAlreadyFilledException("The order already exists in the database.");
            }

            var result = await this.context.Orders.AddAsync(order);
            await this.context.SaveChangesAsync();

            return result.Entity.OrderId;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await this.context.Orders.SingleOrDefaultAsync(x => x.OrderId == id);

            return order ?? new Order();
        }

        public async Task UpdateAsync(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            var dbEntry = this.context.Orders?.FirstOrDefault(p => p.OrderId == order.OrderId);

            if (dbEntry != null)
            {
                dbEntry.Status = order.Status;

                await this.context.SaveChangesAsync();
            }
        }
    }
}
