using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Announcements.Domain.Models;
using EasyJob.API.Announcements.Domain.Services.Communication;


namespace EasyJob.API.Announcements.Domain.Services
{
    public interface IAnnouncementService
    {
        Task<IEnumerable<Announcement>> ListAsync();
        Task<AnnouncementResponse> GetById(int id);
        Task<AnnouncementResponse> SaveAsync(Announcement announcement);
        Task<AnnouncementResponse> UpdateAsync(int id, Announcement announcement);
        Task<AnnouncementResponse> DeleteAsync(int id);
    }
}