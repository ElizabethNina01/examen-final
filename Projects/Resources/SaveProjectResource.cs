using System.ComponentModel.DataAnnotations;

namespace EasyJob.API.Projects.Resources
{
    public class SaveProjectResource
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        [Required]
        [MaxLength(300)]
        public string Url { get; set; }
       
        public string Photo { get; set; }
        
        public int Postulants_id { get; set; }
    }
}