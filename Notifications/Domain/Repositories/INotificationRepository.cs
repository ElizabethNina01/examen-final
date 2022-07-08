using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Notifications.Domain.Models;

namespace EasyJob.API.Notifications.Domain.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> ListAsync();
        Task<Notification> FindById(int id);
        Task AddAsync(Notification notification);
        void Update(Notification notification);
        void Remove(Notification notification);
    }
}