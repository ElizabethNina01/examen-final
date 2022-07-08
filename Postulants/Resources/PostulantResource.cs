namespace EasyJob.API.Postulants.Resources
{
    public class PostulantResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string GithubUser { get; set; }
    }
}