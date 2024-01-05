using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MyWatchShop.Models.Entity
{
    public class ShoppingCart : BaseEntity
    {
        public string? AppUserId { get; set; }
        //public string ProductId { get; set; }
        //Navigation property
        public IList<CartDetail> CartDetails { get; set;} = new List<CartDetail>();
    }
}
