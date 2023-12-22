using System.ComponentModel.DataAnnotations;

namespace MyWatchShop.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public string Email { get; set; }

        [Required] 
        [DataType(DataType.Password)] 
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Mismatch")] 
        public string ConfirmPassword { get; set; }
    }

   
}
