using System.ComponentModel.DataAnnotations;

namespace GLMS.Web.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        [Required]
        public string Region { get; set; } = string.Empty;

        public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}