using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Announcements.Domain.Models;
using EasyJob.API.Announcements.Domain.Repositories;
using EasyJob.API.Announcements.Domain.Services;
using EasyJob.API.Announcements.Domain.Services.Communication;
using EasyJob.API.Applicants.Domain.Repositories;
//**using EasyJob.API.Projects.Domain.Models;
using Go2Climb.API.Domain.Repositories;

namespace EasyJob.API.Announcements.Services
{

    public class AnnouncementService : IAnnouncementService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        private readonly IAnnouncementRepository _announcementRepository;

        public AnnouncementService(IUnitOfWork unitOfWork, IAnnouncementRepository announcementRepository)
        {
            _unitOfWork = unitOfWork;
            _announcementRepository = announcementRepository;
        }

        public  async Task<IEnumerable<Announcement>> ListAsync()
        {
            return await _announcementRepository.ListAsync();
        }

        public async Task<AnnouncementResponse> GetById(int id)
        {
            var existingApplicant = _announcementRepository.FindById(id);
            if (existingApplicant.Result == null)
                return new AnnouncementResponse("The applicant does not exist.");
            
            return new AnnouncementResponse(existingApplicant.Result);
        }

        public  async Task<AnnouncementResponse> SaveAsync(Announcement announcement)
        {
            try
            {
                await _announcementRepository.AddAsync(announcement);
                await _unitOfWork.CompleteAsync();
                return new AnnouncementResponse(announcement);
            }
            catch (Exception e)
            {
                return new AnnouncementResponse($"An error occurred while saving the applicant: {e.Message}");
            }
        }

        public  async Task<AnnouncementResponse> UpdateAsync(int id, Announcement announcement)
        {
            var existingApplicant = await _announcementRepository.FindById(id);
            if (existingApplicant == null)
                return new AnnouncementResponse("Applicant not found");
            existingApplicant.Date = announcement.Date;
            existingApplicant.Description = announcement.Description;
            existingApplicant.Salary = announcement.Salary;
            existingApplicant.Tittle = announcement.Tittle;
            existingApplicant.Type_money = announcement.Type_money;
            existingApplicant.Visible = announcement.Visible;
            try
            {
                _announcementRepository.Update(existingApplicant);
                await _unitOfWork.CompleteAsync();
                return new AnnouncementResponse(existingApplicant);
            }
            catch (Exception e)
            {
                return new AnnouncementResponse($"An error occurred while updating the Applicant: {e.Message}");
            }
        }

        public  async Task<AnnouncementResponse> DeleteAsync(int id)
        {
            var existingApplicant = await _announcementRepository.FindById(id);
            if (existingApplicant == null)
                return new AnnouncementResponse("Applicant not found");
            try
            {
                _announcementRepository.Remove(existingApplicant);
                await _unitOfWork.CompleteAsync();
                return new AnnouncementResponse(existingApplicant);
            }
            catch (Exception e)
            {
                return new AnnouncementResponse($"An error occurred while deleting the applicant: {e.Message}");
            }
        }
    }
}