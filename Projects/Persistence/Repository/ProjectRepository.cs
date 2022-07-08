using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Projects.Domain.Models;
using EasyJob.API.Projects.Domain.Repositories;
using Go2Climb.API.Persistence.Contexts;
using Go2Climb.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EasyJob.API.Projects.Persistence.Repository
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Project>> ListAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> FindById(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
        }

        public void Update(Project project)
        {
            _context.Projects.Update(project);
        }

        public void Remove(Project project)
        {
            _context.Projects.Remove(project);
        }
    }
}