using System.ComponentModel.DataAnnotations;

namespace GLMS.Web.Models
{
    public class ServiceRequest
    {
        public int ServiceRequestId { get; set; }

        [Required]
        [Display(Name = "Request Title")]
        public string RequestTitle { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Request Date")]
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; } = DateTime.Now;

        [Required]
        public string Status { get; set; } = "Pending";

        [Required]
        [Range(1, double.MaxValue)]
        [Display(Name = "Amount in USD")]
        public decimal AmountUsd { get; set; }

        [Display(Name = "Amount in ZAR")]
        public decimal? AmountZar { get; set; }

        [Required]
        [Display(Name = "Contract")]
        public int ContractId { get; set; }

        public Contract? Contract { get; set; }
    }
}