using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Domain.Repositories;
using EasyJob.API.Applicants.Domain.Services;
using EasyJob.API.Applicants.Domain.Services.Communication;
using Go2Climb.API.Domain.Repositories;

namespace EasyJob.API.Applicants.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApplicantRepository _applicantRepository;

        public ApplicantService(IUnitOfWork unitOfWork, IApplicantRepository applicantRepository)
        {
            _unitOfWork = unitOfWork;
            _applicantRepository = applicantRepository;
        }

        public async Task<IEnumerable<Applicant>> ListAsync()
        {
            return await _applicantRepository.ListAsync();
        }

        public async Task<ApplicantResponse> GetById(int id)
        {
            var existingApplicant = _applicantRepository.FindById(id);
            if (existingApplicant.Result == null)
                return new ApplicantResponse("The applicant does not exist.");
            
            return new ApplicantResponse(existingApplicant.Result);
        }

        public async Task<ApplicantResponse> SaveAsync(Applicant applicant)
        {
            try
            {
                await _applicantRepository.AddAsync(applicant);
                await _unitOfWork.CompleteAsync();
                return new ApplicantResponse(applicant);
            }
            catch (Exception e)
            {
                return new ApplicantResponse($"An error occurred while saving the applicant: {e.Message}");
            }
        }

        public async Task<ApplicantResponse> UpdateAsync(int id, Applicant activity)
        {
            var existingApplicant = await _applicantRepository.FindById(id);
            if (existingApplicant == null)
                return new ApplicantResponse("Applicant not found");
            existingApplicant.Name = activity.Name;
            existingApplicant.LastName = activity.LastName;
            existingApplicant.Email = activity.Email;
            existingApplicant.Password = activity.Password;
            existingApplicant.Photo = activity.Photo;
            try
            {
                _applicantRepository.Update(existingApplicant);
                await _unitOfWork.CompleteAsync();
                return new ApplicantResponse(existingApplicant);
            }
            catch (Exception e)
            {
                return new ApplicantResponse($"An error occurred while updating the Applicant: {e.Message}");
            }
        }

        public async Task<ApplicantResponse> DeleteAsync(int id)
        {
            var existingApplicant = await _applicantRepository.FindById(id);
            if (existingApplicant == null)
                return new ApplicantResponse("Applicant not found");
            try
            {
                _applicantRepository.Remove(existingApplicant);
                await _unitOfWork.CompleteAsync();
                return new ApplicantResponse(existingApplicant);
            }
            catch (Exception e)
            {
                return new ApplicantResponse($"An error occurred while deleting the applicant: {e.Message}");
            }
        }
    }
}