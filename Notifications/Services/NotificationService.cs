using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Domain.Services.Communication;
using EasyJob.API.Notifications.Domain.Models;
using EasyJob.API.Notifications.Domain.Repositories;
using EasyJob.API.Notifications.Domain.Services;
using EasyJob.API.Notifications.Domain.Services.Communication;
using Go2Climb.API.Domain.Repositories;

namespace EasyJob.API.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(IUnitOfWork unitOfWork, INotificationRepository notificationRepository)
        {
            _unitOfWork = unitOfWork;
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<Notification>> ListAsync()
        {
            return await _notificationRepository.ListAsync();
        }

        public async Task<NotificationResponse> GetById(int id)
        {
            var existingNotification = _notificationRepository.FindById(id);
            if (existingNotification.Result == null)
                return new NotificationResponse("The notification does not exist.");
            
            return new NotificationResponse(existingNotification.Result);
        }

        public async Task<NotificationResponse> SaveAsync(Notification notification)
        {
            try
            {
                await _notificationRepository.AddAsync(notification);
                await _unitOfWork.CompleteAsync();
                return new NotificationResponse(notification);
            }
            catch (Exception e)
            {
                return new NotificationResponse($"An error occurred while saving the notification: {e.Message}");
            }
        }

        public async Task<NotificationResponse> UpdateAsync(int id, Notification notification)
        {
            var existingNotification = await _notificationRepository.FindById(id);
            if (existingNotification == null)
                return new NotificationResponse("notification not found");
            existingNotification.Title = notification.Title;
            existingNotification.Description = notification.Description;
            existingNotification.Date = notification.Date;
            try
            {
                _notificationRepository.Update(existingNotification);
                await _unitOfWork.CompleteAsync();
                return new NotificationResponse(existingNotification);
            }
            catch (Exception e)
            {
                return new NotificationResponse($"An error occurred while updating the Notification: {e.Message}");
            }
        }

        public async Task<NotificationResponse> DeleteAsync(int id)
        {
            var existingNotification = await _notificationRepository.FindById(id);
            if (existingNotification == null)
                return new NotificationResponse("Notification not found");
            try
            {
                _notificationRepository.Remove(existingNotification);
                await _unitOfWork.CompleteAsync();
                return new NotificationResponse(existingNotification);
            }
            catch (Exception e)
            {
                return new NotificationResponse($"An error occurred while deleting the Notification: {e.Message}");
            }
        }
    }
}