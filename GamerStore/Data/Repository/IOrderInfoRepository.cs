using GamerStore.Models;
using Microsoft.EntityFrameworkCore;

namespace GamerStore.Data.Repository
{
    public interface IOrderInfoRepository
    {
        Task<IEnumerable<OrderInfo>> GetOrderInfoByOrderIdAsync(int id);

        Task SaveOrderInfoAsync(OrderInfo orderInfo);

        Task<IEnumerable<OrderInfo>> GetOrderInfosAsync();
    }

    public class OrderInfoRepository : IOrderInfoRepository
    {
        private readonly StoreDbContext context;

        public OrderInfoRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<OrderInfo>> GetOrderInfoByOrderIdAsync(int id)
        {
            var orderInfo = await this.context.OrderInfos.Where(x => x.OrderId == id).ToListAsync();

            return orderInfo ?? new List<OrderInfo>();
        }

        public async Task<IEnumerable<OrderInfo>> GetOrderInfosAsync()
        {
            var orderInfos = await this.context.OrderInfos.ToListAsync();
            return orderInfos ?? new List<OrderInfo>();
        }

        public async Task SaveOrderInfoAsync(OrderInfo orderInfo)
        {
            ArgumentNullException.ThrowIfNull(orderInfo);

            var result = await this.context.OrderInfos.AddAsync(orderInfo);
            await this.context.SaveChangesAsync();
        }
    }
}
