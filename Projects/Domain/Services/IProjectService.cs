using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Projects.Domain.Models;
using EasyJob.API.Projects.Domain.Services.Communication;

namespace EasyJob.API.Projects.Domain.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> ListAsync();
        Task<ProjectResponse> GetById(int id);
        Task<ProjectResponse> SaveAsync(Project project);
        Task<ProjectResponse> UpdateAsync(int id, Project project);
        Task<ProjectResponse> DeleteAsync(int id);
    }
}