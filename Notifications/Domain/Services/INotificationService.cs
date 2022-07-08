using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Applicants.Domain.Services.Communication;
using EasyJob.API.Notifications.Domain.Models;
using EasyJob.API.Notifications.Domain.Services.Communication;

namespace EasyJob.API.Notifications.Domain.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> ListAsync();
        Task<NotificationResponse> GetById(int id);
        Task<NotificationResponse> SaveAsync(Notification notification);
        Task<NotificationResponse> UpdateAsync(int id, Notification notification);
        Task<NotificationResponse> DeleteAsync(int id);
    }
}