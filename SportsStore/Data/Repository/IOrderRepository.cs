using GamerStore.Models;

namespace GamerStore.Data.Repository
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }

        void SaveOrder(Order order);
    }
}
