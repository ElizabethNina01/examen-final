using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Applicants.Domain.Repositories;
using EasyJob.API.Projects.Domain.Models;
using EasyJob.API.Projects.Domain.Repositories;
using EasyJob.API.Projects.Domain.Services;
using EasyJob.API.Projects.Domain.Services.Communication;
using Go2Climb.API.Domain.Repositories;

namespace EasyJob.API.Projects.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IUnitOfWork unitOfWork, IProjectRepository projectRepository)
        {
            _unitOfWork = unitOfWork;
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> ListAsync()
        {
            return await _projectRepository.ListAsync();
        }

        public async Task<ProjectResponse> GetById(int id)
        {
            var existingProject = _projectRepository.FindById(id);
            if (existingProject.Result == null)
                return new ProjectResponse("The Project does not exist.");
            
            return new ProjectResponse(existingProject.Result);
        }

        public async Task<ProjectResponse> SaveAsync(Project project)
        {
            try
            {
                await _projectRepository.AddAsync(project);
                await _unitOfWork.CompleteAsync();
                return new ProjectResponse(project);
            }
            catch (Exception e)
            {
                return new ProjectResponse($"An error occurred while saving the project: {e.Message}");
            }
        }

        public async Task<ProjectResponse> UpdateAsync(int id, Project project)
        {
            var existingProject = await _projectRepository.FindById(id);
            if (existingProject == null)
                return new ProjectResponse("project not found");
            existingProject.Description = project.Description;
            existingProject.Photo= project.Photo;
            existingProject.Postulants_id = project.Postulants_id;
            existingProject.Title = project.Title;
            existingProject.Url = project.Url;
            try
            {
                _projectRepository.Update(existingProject);
                await _unitOfWork.CompleteAsync();
                return new ProjectResponse(existingProject);
            }
            catch (Exception e)
            {
                return new ProjectResponse($"An error occurred while updating the project: {e.Message}");
            }
        }

        public async Task<ProjectResponse> DeleteAsync(int id)
        {
            var existingProject = await _projectRepository.FindById(id);
            if (existingProject == null)
                return new ProjectResponse("project not found");
            try
            {
                _projectRepository.Remove(existingProject);
                await _unitOfWork.CompleteAsync();
                return new ProjectResponse(existingProject);
            }
            catch (Exception e)
            {
                return new ProjectResponse($"An error occurred while deleting the project: {e.Message}");
            }
        }
    }
}