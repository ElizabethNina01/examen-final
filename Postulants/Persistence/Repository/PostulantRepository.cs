using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Postulants.Domain.Models;
using EasyJob.API.Postulants.Domain.Repository;
using Go2Climb.API.Persistence.Contexts;
using Go2Climb.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EasyJob.API.Postulants.Persistence.Repository
{
    public class PostulantRepository : BaseRepository, IPostulantRepository
    {
        public PostulantRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Postulant>> ListAsync()
        {
            return await _context.Postulants.ToListAsync();
        }

        public async Task<Postulant> FindById(int id)
        {
            return await _context.Postulants.FindAsync(id);
        }

        public async Task AddAsync(Postulant postulant)
        {
            await _context.Postulants.AddAsync(postulant);
        }

        public void Update(Postulant postulant)
        {
            _context.Postulants.Update(postulant);
        }

        public void Remove(Postulant postulant)
        {
            _context.Postulants.Remove(postulant);
        }
    }
}