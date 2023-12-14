namespace MyWatchShop.Models
{
    public class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime DateDeleted { get; set; }

    }
}
