namespace MyWatchShop.Models.ViewModels
{
    public class UserToReturnViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ImageUrl {  get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
