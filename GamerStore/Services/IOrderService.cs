using GamerStore.Data.Repository;
using GamerStore.Models;
using GamerStore.Models.DTO;

namespace GamerStore.Services
{
    public interface IOrderService
    {
        OrderStatus[] GetAllowedStatuses(OrderStatus currentStatus);

        Task ChangeStatusAsync(int orderId, OrderStatus newStatus);

        Task SaveOrderAsync(Order order, IEnumerable<CartLine> cartLines);

        Task<List<OrderDTO>> GetOrdersAsync();
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderInfoRepository orderInfoRepository;
        private readonly IStoreRepository storeRepository;

        private readonly Dictionary<OrderStatus, OrderStatus[]> allowedStatuses;

        public OrderService(
            IOrderRepository orderRepository,
            IOrderInfoRepository orderInfoRepository,
            IStoreRepository storeRepository)
        {
            this.orderInfoRepository = orderInfoRepository;
            this.orderRepository = orderRepository;
            this.storeRepository = storeRepository;

            this.allowedStatuses = new Dictionary<OrderStatus, OrderStatus[]>
            {
                { OrderStatus.Pending, new[] { OrderStatus.Confirmed, OrderStatus.Cancelled } },
                { OrderStatus.Confirmed, new[] { OrderStatus.Processing, OrderStatus.Cancelled } },
                { OrderStatus.Processing, new[] { OrderStatus.Shipped, OrderStatus.Cancelled } },
                { OrderStatus.Shipped, new[] { OrderStatus.Delivered } },
                { OrderStatus.Delivered, Array.Empty<OrderStatus>() },
                { OrderStatus.Cancelled, Array.Empty<OrderStatus>() },
            };
        }

        public async Task<List<OrderDTO>> GetOrdersAsync()
        {
            var orders = await this.orderRepository.GetOrdersAsync();
            var orderInfos = await this.orderInfoRepository.GetOrderInfosAsync();
            var products = await this.storeRepository.GetProductsAsync();

            var redactedOrders = orders.Select(order => new OrderDTO
            {
                OrderId = order.OrderId,
                CustomerInfo = new CustomerDTO(),
                GiftWrap = order.GiftWrap,
                Status = order.Status,
                Total = order.Total,
                OrderInfoDTOs = orderInfos
                    .Where(oi => oi.OrderId == order.OrderId)
                    .Select(oi => new OrderInfoDTO
                    {
                        ProductName = products.FirstOrDefault(p => p.Id == oi.ProductId) is { } p
                            ? string.Join(" ", p?.Brand?.Name, p?.Model, p?.Title)
                            : string.Empty,
                        Quantity = oi.Quantity,
                    }),
            }).ToList();

            return redactedOrders ?? new List<OrderDTO>();
        }

        public OrderStatus[] GetAllowedStatuses(OrderStatus currentStatus)
        {
            if (this.allowedStatuses.TryGetValue(currentStatus, out var allowed))
            {
                return allowed;
            }

            return Array.Empty<OrderStatus>();
        }

        public async Task ChangeStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await this.orderRepository.GetOrderByIdAsync(orderId);
            ValidateTransition(order.Status, newStatus);
            order.Status = newStatus;

            await this.orderRepository.UpdateAsync(order);
        }

        private void ValidateTransition(OrderStatus current, OrderStatus next)
        {
            if (!this.allowedStatuses[current].Contains(next))
            {
                throw new InvalidOperationException($"Cannot transition from {current} to {next}");
            }
        }

        public async Task SaveOrderAsync(Order order, IEnumerable<CartLine> cartLines)
        {
            ArgumentNullException.ThrowIfNull(order);

            int orderId = await this.orderRepository.SaveOrderAsync(order);

            foreach (var cartLine in cartLines)
            {
                var orderInfo = new OrderInfo
                {
                    OrderId = orderId,
                    ProductId = cartLine.Product.Id,
                    Quantity = cartLine.Quantity,
                };

                await this.orderInfoRepository.SaveOrderInfoAsync(orderInfo);
            }
        }
    }
}
