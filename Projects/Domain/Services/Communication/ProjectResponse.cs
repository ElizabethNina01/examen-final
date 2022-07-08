using EasyJob.API.Projects.Domain.Models;
using Go2Climb.API.Domain.Services.Communication;

namespace EasyJob.API.Projects.Domain.Services.Communication
{
    public class ProjectResponse : BaseResponse<Project>
    {
        public ProjectResponse(string message) : base(message)
        {
            
        }

        public ProjectResponse(Project resource) : base(resource)
        {
            
        }
    }
}