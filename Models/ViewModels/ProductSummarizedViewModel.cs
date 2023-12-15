namespace MyWatchShop.Models.ViewModels
{
    public class ProductSummarizedViewModel
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public int Stars { get; set; }
    }
}
