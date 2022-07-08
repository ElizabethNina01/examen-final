using AutoMapper;
using EasyJob.API.Announcements.Domain.Models;
using EasyJob.API.Announcements.Resources;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Resources;
using EasyJob.API.Postulants.Domain.Models;
using EasyJob.API.Postulants.Resources;
using EasyJob.API.Interviews.Domain.Models;
using EasyJob.API.Interviews.Resources;
using EasyJob.API.Messages.Domain.Models;
using EasyJob.API.Messages.Resources;
using EasyJob.API.Notifications.Domain.Models;
using EasyJob.API.Notifications.Resources;
using EasyJob.API.Payments.Domain.Models;
using EasyJob.API.Payments.Resources;
using EasyJob.API.Projects.Domain.Models;
using EasyJob.API.Projects.Resources;
using Go2Climb.API.Domain.Services.Communication;

namespace Go2Climb.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Applicant, ApplicantResource>();
            CreateMap<Postulant, PostulantResource>();
            CreateMap<Announcement, AnnouncementResource>();
            CreateMap<Message, MessagesResources>();
            CreateMap<Interview, InterviewResources>();
            CreateMap<Notification, NotificationResource>();
            CreateMap<Payment, PaymentResource>();
            CreateMap<Project, ProjectResource>();
        }
    }
}