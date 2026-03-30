using GamerStore.Infrastructure;
using GamerStore.Models.DTO;

namespace GamerStore.Models
{
    public class SessionCart : Cart
    {
        [Newtonsoft.Json.JsonIgnore]
        public ISession? Session { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        public override void AddItem(ProductDTO product, int quantity)
        {
            base.AddItem(product, quantity);
            this.Session?.SetJson("Cart", this);
        }

        public override void RemoveLine(ProductDTO product)
        {
            base.RemoveLine(product);
            this.Session?.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            this.Session?.Remove("Cart");
        }
    }
}
