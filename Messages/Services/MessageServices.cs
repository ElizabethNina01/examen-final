using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Domain.Repositories;
using EasyJob.API.Applicants.Domain.Services.Communication;
using EasyJob.API.Messages.Domain.Models;
using EasyJob.API.Messages.Domain.Repositories;
using EasyJob.API.Messages.Services;
using EasyJob.API.Messages.Services.Communication;
using Go2Climb.API.Domain.Repositories;

namespace EasyJob.API.Messages.Services
{
    public class MessageServices :IMessageServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagesRepository _messagesRepository;

        public MessageServices(IUnitOfWork unitOfWork, IMessagesRepository messagesRepository)
        {
            _unitOfWork = unitOfWork;
            _messagesRepository = messagesRepository;
        }

        public async Task<IEnumerable<Message>> ListAsync()
        {
            return await _messagesRepository.ListAsync();
        }

        public async Task<MessageResponse> GetById(int id)
        {
            var existingMessage = _messagesRepository.FindById(id);
            if (existingMessage.Result == null)
                return new MessageResponse("The applicant does not exist.");
            
            return new MessageResponse(existingMessage.Result);
        }

        public async Task<MessageResponse> SaveAsync(Message message)
        {
            try
            {
                await _messagesRepository.AddAsync(message);
                await _unitOfWork.CompleteAsync();
                return new MessageResponse(message);
            }
            catch (Exception e)
            {
                return new MessageResponse($"An error occurred while saving the applicant: {e.Message}");
            }
        }

        public async Task<MessageResponse> UpdateAsync(int id, Message message)
        {
            var existingMessage = await _messagesRepository.FindById(id);
            if (existingMessage == null)
                return new MessageResponse("Applicant not found");
            existingMessage.Description= message.Description;
            existingMessage.Date= message.Date;
            
            try
            {
                _messagesRepository.Update(existingMessage);
                await _unitOfWork.CompleteAsync();
                return new MessageResponse(existingMessage);
            }
            catch (Exception e)
            {
                return new MessageResponse($"An error occurred while saving the applicant: {e.Message}");
            }
        }

        public async Task<MessageResponse> DeleteAsync(int id)
        {
            var existingMessage = await _messagesRepository.FindById(id);
            if (existingMessage == null)
                return new MessageResponse("Applicant not found");
            try
            {
                _messagesRepository.Remove(existingMessage);
                await _unitOfWork.CompleteAsync();
                return new MessageResponse(existingMessage);
            }
            catch (Exception e)
            {
                return new MessageResponse($"An error occurred while deleting the applicant: {e.Message}");
            }
        }
    }
}