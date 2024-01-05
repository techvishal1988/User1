using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class RegistrationRequestModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [RegularExpression("^(?!\\s+)(?!.*\\s+$)(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[$^*.[\\]{}()?\"!@#%&\\\\,><':;|_~`=+\\- ])[A-Za-z0-9$^*.[\\]{}()?\"!@#%&\\\\,><':;|_~`=+\\- ]{8,256}$", ErrorMessage = "Passwords must be 8 character minimum length, contains at least 1 number, contains at least 1 lowercase letter, contains at least 1 uppercase letter, contains at least 1 special character.")]        
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [RegularExpression("^(?!\\s+)(?!.*\\s+$)(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[$^*.[\\]{}()?\"!@#%&\\\\,><':;|_~`=+\\- ])[A-Za-z0-9$^*.[\\]{}()?\"!@#%&\\\\,><':;|_~`=+\\- ]{8,256}$", ErrorMessage = "Confirm passwords 8 character minimum length, contains at least 1 number, contains at least 1 lowercase letter, contains at least 1 uppercase letter, contains at least 1 special character.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
