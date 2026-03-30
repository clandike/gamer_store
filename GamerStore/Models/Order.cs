using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GamerStore.Models
{
    public enum OrderStatus
    {
        Pending,      // створено, але не підтверджено
        Confirmed,    // підтверджено користувачем
        Processing,   // обробляється
        Shipped,      // відправлено
        Delivered,    // доставлено
        Cancelled,    // скасовано
    }

    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();

        public bool GiftWrap { get; set; }

        public decimal Total { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public IEnumerable<OrderInfo> OrderInfos { get; set; }
    }

    public class OrderInfo
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public Order Order { get; set; }
    }
}
