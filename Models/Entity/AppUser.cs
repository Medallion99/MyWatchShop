using Microsoft.AspNetCore.Identity;

namespace MyWatchShop.Models.Entity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        //navigation
        public IList<Review> Reviews { get; set; } = new List<Review>();
        public IList<Rating> Ratings { get; set; } = new List<Rating>();
    }
}
