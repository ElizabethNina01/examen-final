using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Domain.Repositories;
using Go2Climb.API.Persistence.Contexts;
using Go2Climb.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EasyJob.API.Applicants.Persistence.Repository
{
    public class ApplicantRepository : BaseRepository, IApplicantRepository
    {
        public ApplicantRepository(AppDbContext context) : base(context)
        {
        }
        
        public async Task<IEnumerable<Applicant>> ListAsync()
        {
            return await _context.Applicants.ToListAsync();
        }

        public async Task<Applicant> FindById(int id)
        {
            return await _context.Applicants.FindAsync(id);
        }

        public async Task AddAsync(Applicant applicant)
        {
            await _context.Applicants.AddAsync(applicant);        }

        public void Update(Applicant applicant)
        {
            _context.Applicants.Update(applicant);
        }

        public void Remove(Applicant applicant)
        {
            _context.Applicants.Remove(applicant);
        }
    }
}