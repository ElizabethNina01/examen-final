using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Messages.Domain.Models;
using EasyJob.API.Interviews.Domain.Repositories;
using EasyJob.API.Messages.Domain.Repositories;
using Go2Climb.API.Persistence.Contexts;
using Go2Climb.API.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EasyJob.API.Messages.Persistence.Repository
{
    public class MessageRepository : BaseRepository,IMessagesRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Message>> ListAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<Message> FindById(int id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task AddAsync(Message message)
        {
            await _context.Messages.AddAsync(message);  
        }

        public void Update(Message message)
        {
            _context.Messages.Update(message);
        }

        public void Remove(Message message)
        {
            _context.Messages.Remove(message);
        }
    }
}