using System.ComponentModel.DataAnnotations;

namespace GLMS.Web.Models
{
    public class Contract
    {
        public int ContractId { get; set; }

        [Required]
        [Display(Name = "Contract Number")]
        public string ContractNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Service Level")]
        public string ServiceLevel { get; set; } = string.Empty;

        public string? SignedAgreementFileName { get; set; }

        [Required]
        [Display(Name = "Client")]
        public int ClientId { get; set; }

        public Client? Client { get; set; }

        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
    }
}