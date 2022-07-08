using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Interviews.Domain.Models;
using EasyJob.API.Interviews.Domain.Repositories;
using EasyJob.API.Interviews.Domain.Repositories;
using Go2Climb.API.Persistence.Contexts;
using Go2Climb.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EasyJob.API.Interviews.Persistence.Repository

{
    public class InterviewRepository : BaseRepository, IInterviewsRepository
    {
        public async Task<IEnumerable<Interview>> ListAsync()
        {
            return await _context.Interviews.ToListAsync();
        }

        public async Task<Interview> FindById(int id)
        {
            return await _context.Interviews.FindAsync(id);
        }

        public async Task AddAsync(Interview interview)
        {
            await _context.Interviews.AddAsync(interview);  
        }

        public void Update(Interview interview)
        {
            _context.Interviews.Update(interview);
        }

        public void Remove(Interview interview)
        {
            _context.Interviews.Remove(interview);
        }

        public InterviewRepository(AppDbContext context) : base(context)
        {
        }
    }
    
    
}