namespace MyWatchShop.Models.ViewModels
{
    public class HomeViewModel
    {
        public Showcase BestSeller {  get; set; }
        public Showcase AllProduct { get; set; }
        public IList<GetUserCartViewModel> GetUserCartViewModel { get; set; } = new List<GetUserCartViewModel>();
    }

    public class Showcase
    {
        public IList<ProductSummarizedViewModel> ProductList { get; set; } = new List<ProductSummarizedViewModel>();
        public int Product { get; set; }
    }
}
