using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Projects.Domain.Models;

namespace EasyJob.API.Projects.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> ListAsync();
        Task<Project> FindById(int id);
        Task AddAsync(Project project);
        void Update(Project  project);
        void Remove(Project  project);
    }
}