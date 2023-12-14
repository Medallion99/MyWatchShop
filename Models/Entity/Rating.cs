namespace MyWatchShop.Models.Entity
{
    public class Rating : BaseEntity
    {
        public int Stars { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string AppUserId { get; set; }
        public string ProductId { get; set; }
        //Navigation property
        public AppUser AppUser { get; set; }
        public Product Product { get; set; }


    }
}
