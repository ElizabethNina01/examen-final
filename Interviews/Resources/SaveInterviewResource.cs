using System.ComponentModel.DataAnnotations;
namespace EasyJob.API.Interviews.Resources
{
    public class SaveInterviewResource
    {
        [Required]
        [MaxLength(50)]
        public string Date { get; set; }
        
        [Required]
        [MaxLength(120)]
        public string  Hora { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string  Link { get; set; }
        
    }
}

