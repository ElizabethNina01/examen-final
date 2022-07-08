
using EasyJob.API.Announcements.Domain.Models;
using Go2Climb.API.Domain.Services.Communication;

namespace EasyJob.API.Announcements.Domain.Services.Communication
{
    public class AnnouncementResponse : BaseResponse<Announcement>
    {
        public AnnouncementResponse(string message) : base(message)
        {
            
        }

        public AnnouncementResponse(Announcement resource) : base(resource)
        {
            
        }
    }
}