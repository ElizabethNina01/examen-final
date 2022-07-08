using System.ComponentModel.DataAnnotations;

namespace EasyJob.API.Applicants.Resources
{
    public class SaveApplicantResource
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        
        [Required]
        [MaxLength(120)]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Password { get; set; }
        
        public string Photo { get; set; }
    }
}