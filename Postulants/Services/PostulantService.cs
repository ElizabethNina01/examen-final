using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Applicants.Domain.Repositories;
using EasyJob.API.Postulants.Domain;
using EasyJob.API.Postulants.Domain.Communication;
using EasyJob.API.Postulants.Domain.Models;
using EasyJob.API.Postulants.Domain.Repository;
using Go2Climb.API.Domain.Repositories;

namespace EasyJob.API.Postulants.Services
{
    public class PostulantService : IPostulantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostulantRepository _postulantRepository;

        public PostulantService(IUnitOfWork unitOfWork, IPostulantRepository postulantRepository)
        {
            _unitOfWork = unitOfWork;
            _postulantRepository = postulantRepository;
        }

        public async Task<IEnumerable<Postulant>> ListAsync()
        {
            return await _postulantRepository.ListAsync();
        }

        public async Task<PostulantResponse> GetById(int id)
        {
            var existingApplicant = _postulantRepository.FindById(id);
            if (existingApplicant.Result == null)
                return new PostulantResponse("The applicant does not exist.");
            
            return new PostulantResponse(existingApplicant.Result);
        }

        public async Task<PostulantResponse> SaveAsync(Postulant postulant)
        {
            try
            {
                await _postulantRepository.AddAsync(postulant);
                await _unitOfWork.CompleteAsync();
                return new PostulantResponse(postulant);
            }
            catch (Exception e)
            {
                return new PostulantResponse($"An error occurred while saving the applicant: {e.Message}");
            }
        }

        public async Task<PostulantResponse> UpdateAsync(int id, Postulant postulant)
        {
            var existingApplicant = await _postulantRepository.FindById(id);
            if (existingApplicant == null)
                return new PostulantResponse("Applicant not found");
            existingApplicant.Name = postulant.Name;
            existingApplicant.LastName = postulant.LastName;
            existingApplicant.Email = postulant.Email;
            existingApplicant.Password = postulant.Password;
            existingApplicant.Description = postulant.Description;
            existingApplicant.GithubUser = postulant.GithubUser;
            try
            {
                _postulantRepository.Update(existingApplicant);
                await _unitOfWork.CompleteAsync();
                return new PostulantResponse(existingApplicant);
            }
            catch (Exception e)
            {
                return new PostulantResponse($"An error occurred while updating the Applicant: {e.Message}");
            }
        }

        public async Task<PostulantResponse> DeleteAsync(int id)
        {
            var existingApplicant = await _postulantRepository.FindById(id);
            if (existingApplicant == null)
                return new PostulantResponse("Applicant not found");
            try
            {
                _postulantRepository.Remove(existingApplicant);
                await _unitOfWork.CompleteAsync();
                return new PostulantResponse(existingApplicant);
            }
            catch (Exception e)
            {
                return new PostulantResponse($"An error occurred while deleting the applicant: {e.Message}");
            }
        }
    }
}