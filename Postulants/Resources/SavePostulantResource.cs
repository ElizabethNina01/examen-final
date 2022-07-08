using System.ComponentModel.DataAnnotations;

namespace EasyJob.API.Postulants.Resources
{
    public class SavePostulantResource
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        
        [MaxLength(100)]
        public string Description { get; set; }
        
        [Required]
        [MaxLength(120)]
        public string Email { get; set; }
        
        [Required]
        [MaxLength(25)]
        public string Password { get; set; }
        
        [Required]
        [MaxLength(25)]
        public string GithubUser { get; set; }
    }
}