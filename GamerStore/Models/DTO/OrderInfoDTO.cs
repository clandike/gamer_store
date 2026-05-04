namespace GamerStore.Models.DTO
{
    /// <summary>
    /// Specifies the available loyalty levels for a customer or user.
    /// </summary>
    /// <remarks>Use this enumeration to represent and compare customer loyalty tiers, such as None, Bronze,
    /// Silver, or Gold. The numeric values may be used for sorting or prioritization, but the meaning of each tier is
    /// defined by the application context.</remarks>
    public enum Loyalty
    {
        /// <summary>
        /// Indicates that no options are specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// Represents the gold membership level.
        /// </summary>
        Gold = 1,

        /// <summary>
        /// Represents the Silver membership level.
        /// </summary>
        Silver = 2,

        /// <summary>
        /// Represents the bronze membership level or status.
        /// </summary>
        Bronze = 3,
    }

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
        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Zip { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public Loyalty LoyaltyLevel { get; set; }

        public bool IsLogged { get; set; } = false;
    }
}
