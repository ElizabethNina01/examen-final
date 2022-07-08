using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Domain.Services;
using EasyJob.API.Applicants.Resources;
using EasyJob.API.Messages.Domain.Models;
using EasyJob.API.Messages.Resources;
using EasyJob.API.Messages.Services;
using Go2Climb.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EasyJob.API.Messages.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageServices _messageServices;
        private readonly IMapper _mapper;

        public MessagesController(IMessageServices messageServices, IMapper mapper)
        {
            _messageServices = messageServices;
            _mapper = mapper;
        }


        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All messages",
            Description = "Get All Messages already stored",
            Tags = new[] {"Messages"})]
        public async Task<IEnumerable<MessagesResources>> GetAllAsync()
        {
            var messages = await _messageServices.ListAsync();
            var resources = _mapper.Map<IEnumerable<Message>, IEnumerable<MessagesResources>>(messages);
            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get Message By Id",
            Description = "Get A message From The Database Identified By Its Id.",
            Tags = new[] {"Messages"})]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _messageServices.GetById(id);
        
            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Resource);
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Register A message",
            Description = "Add A messages to Database.",
            Tags = new[] {"Messages"})]
        public async Task<IActionResult> PostAsync([FromBody] SaveMessageResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var message = _mapper.Map<SaveMessageResource, Message>(resource);
            var result = await _messageServices.SaveAsync(message);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var messagesResources = _mapper.Map<Message, MessagesResources>(result.Resource);

            return Ok(messagesResources);
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Edit A message",
            Description = "Edit A message of the Database.",
            Tags = new[] {"Messages"})]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveMessageResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var message = _mapper.Map<SaveMessageResource, Message>(resource);

            var result = await _messageServices.UpdateAsync(id, message);
            
            if (!result.Success)
                return BadRequest(result.Message);

            var messagesResources = _mapper.Map<Message, MessagesResources>(result.Resource);

            return Ok(messagesResources);
        }
        
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete A message",
            Description = "Delete A message of the Database.",
            Tags = new[] {"Messages"})]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _messageServices.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var messagesResources = _mapper.Map<Message, MessagesResources>(result.Resource);
            
            return Ok(messagesResources);
        }
    }
    
}