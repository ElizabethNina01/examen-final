using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Announcements.Domain.Models;

namespace EasyJob.API.Announcements.Domain.Repositories
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<Announcement>> ListAsync();
        Task<Announcement> FindById(int id);
        Task AddAsync(Announcement announcements);
        void Update(Announcement announcements);
        void Remove(Announcement announcements);
    }
}