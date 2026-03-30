namespace GamerStore.Models.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public CustomerDTO CustomerInfo { get; set; }

        public bool GiftWrap { get; set; }

        public decimal Total { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public IEnumerable<OrderInfoDTO> OrderInfoDTOs { get; set; }
    }

    public class OrderInfoDTO
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }

    public class CustomerDTO
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Zip { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public Loyalty LoyaltyLevel { get; set; }
    }

    public enum Loyalty
    {
        None,
        Bronze,
        Silver,
        Gold
    }
}
