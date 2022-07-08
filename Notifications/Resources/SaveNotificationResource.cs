using System.ComponentModel.DataAnnotations;

namespace EasyJob.API.Notifications.Resources
{
    public class SaveNotificationResource
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string Date { get; set; }

    }
}