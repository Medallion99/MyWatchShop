namespace MyWatchShop.Models.Entity
{
    public class Review : BaseEntity
    {
        public string ReviewerName { get; set; } = "";
        public string ReviewerEmail { get; set; } = "";
        public string AppUserId { get; set; } = "";
        public string ProductId { get; set; } = "";


        //navigation
        public AppUser AppUser { get; set; }
        public Product Product { get; set; }


    }
}
