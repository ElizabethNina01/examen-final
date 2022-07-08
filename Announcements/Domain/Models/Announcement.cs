namespace EasyJob.API.Announcements.Domain.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public double Salary { get; set; }
        public string Date { get; set; }
        public string Type_money { get; set; }
        public int Visible { get; set; }
        
        
    }
}