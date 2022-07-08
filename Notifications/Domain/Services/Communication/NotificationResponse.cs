using EasyJob.API.Notifications.Domain.Models;
using Go2Climb.API.Domain.Services.Communication;

namespace EasyJob.API.Notifications.Domain.Services.Communication
{
    public class NotificationResponse : BaseResponse<Notification>
    {
        public NotificationResponse(string message) : base(message)
        {
        }

        public NotificationResponse(Notification resource) : base(resource)
        {
        }
    }
}