namespace MyWatchShop.Models.ViewModels
{
    public class ManageUserViewModel
    {
        public string RoleName { get; set; }
        public IList<UserToReturnViewModel> TableData { get; set; } = new List<UserToReturnViewModel>();
        public UserToReturnViewModel UserDetails { get; set; }

    }
}
