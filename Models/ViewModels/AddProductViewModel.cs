namespace MyWatchShop.Models.DTOS
{
    public class AddProductViewModel
    {
        public string Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        
    }
}
