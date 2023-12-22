using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MyWatchShop.Models.Entity
{
    public class Cart : BaseEntity
    {
        public string ProductName { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string ImageUrl { get; set; }
        public string AppUserId { get; set; }
        public string ProductId { get; set; }
        
        //Navigation property
        public IList<Product> Products { get; set;}
    }
}
