using System.ComponentModel.DataAnnotations;

namespace EasyJob.API.Payments.Resources
{
    public class SavePaymentResource
    {
        [Required]
        [MaxLength(100)]
        public string Method { get; set; }
    }
}