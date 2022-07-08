namespace EasyJob.API.Projects.Resources
{
    public class ProjectResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Photo { get; set; }
        public int Postulants_id { get; set; }
    }
}