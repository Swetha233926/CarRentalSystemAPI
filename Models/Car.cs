using System.ComponentModel.DataAnnotations;

namespace CarRentalSystemAPI.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Make is required.")]
        [StringLength(50, ErrorMessage = "Make cannot exceed 50 characters.")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        [StringLength(50, ErrorMessage = "Model cannot exceed 50 characters.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        [Range(1886, 2100, ErrorMessage = "Year must be between 1886 and 2100.")] // First car invented in 1886
        public int Year { get; set; }

        [Required(ErrorMessage = "Price per day is required.")]
        [Range(1, 10000, ErrorMessage = "Price per day must be between 1 and 10,000.")]
        public decimal PricePerDay { get; set; }

        public bool IsAvailable { get; set; } = true; // Default value set to true
    }
}
