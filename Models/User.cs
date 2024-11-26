using System.ComponentModel.DataAnnotations;

namespace CarRentalSystemAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, ErrorMessage = "Username cannot exceed 100 characters.")]
        public string User_name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string User_email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string User_password { get; set; }

        [Required(ErrorMessage = "User role is required.")]
        [RegularExpression("^(Admin|User)$", ErrorMessage = "User role must be either 'Admin' or 'User'.")]
        public string User_role { get; set; }
    }
}
