using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Domain.Repositories;
using EasyJob.API.Applicants.Domain.Services.Communication;
using EasyJob.API.Interviews.Domain.Models;
using EasyJob.API.Interviews.Domain.Repositories;
using EasyJob.API.Interviews.Domain.Services;
using EasyJob.API.Interviews.Domain.Services.Communication;
using EasyJob.API.Interviews.Services;
using Go2Climb.API.Domain.Repositories;

namespace EasyJob.API.Interviews.Services
{
    public class InterviewServices: IInterviewServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInterviewsRepository _interviewsRepository;

        public InterviewServices(IUnitOfWork unitOfWork,  IInterviewsRepository interviewsRepository)
        {
            _unitOfWork = unitOfWork;
            _interviewsRepository = interviewsRepository;
        
        }

        public async Task<IEnumerable<Interview>> ListAsync()
        {
            return await _interviewsRepository.ListAsync();
        }

        public async Task<InterviewResponse> GetById(int id)
        {
            var existingInterview = _interviewsRepository.FindById(id);
            if (existingInterview.Result == null)
                return new InterviewResponse("The applicant does not exist.");
            
            return new InterviewResponse(existingInterview.Result);
        }

        public async Task<InterviewResponse> SaveAsync(Interview interview)
        {
            try
            {
                await _interviewsRepository.AddAsync(interview);
                await _unitOfWork.CompleteAsync();
                return new InterviewResponse(interview);
            }
            catch (Exception e)
            {
                return new InterviewResponse($"An error occurred while saving the applicant: {e.Message}");
            }
        }

        public async Task<InterviewResponse> UpdateAsync(int id, Interview interview)
        {
            var existingInterview = await _interviewsRepository.FindById(id);
            if (existingInterview == null)
                return new InterviewResponse("Interview not found");
            existingInterview.Date= interview.Date;
            existingInterview.Hora= interview.Hora;
            existingInterview.Link= interview.Link;
            
            try
            {
                _interviewsRepository.Update(existingInterview);
                await _unitOfWork.CompleteAsync();
                return new InterviewResponse(existingInterview);
            }
            catch (Exception e)
            {
                return new InterviewResponse($"An error occurred while updating the Applicant: {e.Message}");
            }
        }

        public async Task<InterviewResponse> DeleteAsync(int id)
        {
            var existingInterview = await _interviewsRepository.FindById(id);
            if (existingInterview == null)
                return new InterviewResponse("Applicant not found");
            try
            {
                _interviewsRepository.Remove(existingInterview);
                await _unitOfWork.CompleteAsync();
                return new InterviewResponse(existingInterview);
            }
            catch (Exception e)
            {
                return new InterviewResponse($"An error occurred while deleting the applicant: {e.Message}");
            }
        }
    }
}