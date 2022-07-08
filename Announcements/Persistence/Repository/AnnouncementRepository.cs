using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Announcements.Domain.Models;
using EasyJob.API.Announcements.Domain.Repositories;
using Go2Climb.API.Persistence.Contexts;
using Go2Climb.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EasyJob.API.Announcements.Persistence.Repository
{
    public class AnnouncementRepository :BaseRepository, IAnnouncementRepository
    {
        public  AnnouncementRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Announcement>> ListAsync()
        {
            return await _context.Announcements.ToListAsync();
        }

        public async Task<Announcement> FindById(int id)
        {
            return await _context.Announcements.FindAsync(id);
        }

        public async Task AddAsync(Announcement announcements)
        {
            await _context.Announcements.AddAsync(announcements);
        }

        public void Update(Announcement announcements)
        {
            _context.Announcements.Update(announcements);
        }

        public void Remove(Announcement announcements)
        {
            _context.Announcements.Remove(announcements);
        }
    }
}