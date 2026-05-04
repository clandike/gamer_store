using GamerStore.Models.DTO;

namespace GamerStore.Services
{
    public interface IUserService
    {


        CustomerDTO? ValidateUser(string username, string password);

        IEnumerable<CustomerDTO> GetAllUsers();
    }

    public class UserService : IUserService
    {
        public static CustomerDTO User { get; set; } = new CustomerDTO() { LoyaltyLevel = Loyalty.None };

        private readonly List<CustomerDTO> customer;

        public UserService()
        {
            this.customer = new List<CustomerDTO>
            {
                new () { Username = "user", PasswordHash = "1234", LoyaltyLevel = Loyalty.None},
                new () { Username = "userSilver", PasswordHash = "1234", LoyaltyLevel = Loyalty.Silver },
                new () { Username = "userBronze", PasswordHash = "1234", LoyaltyLevel = Loyalty.Bronze },
                new () { Username = "userGold", PasswordHash = "1234", LoyaltyLevel = Loyalty.Gold },
            };
        }

        public CustomerDTO? ValidateUser(string username, string password)
        {
            var user = this.customer.SingleOrDefault(u => u.Username == username);
            if (user != null && user.PasswordHash == password)
            {
                User = user;
                user.IsLogged = true;

                return user;
            }

            return null;
        }

        public IEnumerable<CustomerDTO> GetAllUsers() => this.customer;
    }
}
