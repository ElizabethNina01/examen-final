using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EasyJob.API.Notifications.Domain.Models;
using EasyJob.API.Notifications.Domain.Services;
using EasyJob.API.Notifications.Resources;
using Go2Climb.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EasyJob.API.Notifications.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/[controller]")]
    
    public class NotificationController : ControllerBase
    {
         private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All notifications",
            Description = "Get All notifications already stored",
            Tags = new[] {"Notifications"})]
        public async Task<IEnumerable<NotificationResource>> GetAllAsync()
        {
            var notifications = await _notificationService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(notifications);
            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get notifications By Id",
            Description = "Get A notification From The Database Identified By Its Id.",
            Tags = new[] {"Notifications"})]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _notificationService.GetById(id);
        
            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Resource);
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Register A notification",
            Description = "Add A notification to Database.",
            Tags = new[] {"Notifications"})]
        public async Task<IActionResult> PostAsync([FromBody] SaveNotificationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var notification = _mapper.Map<SaveNotificationResource, Notification>(resource);
            var result = await _notificationService.SaveAsync(notification);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);

            return Ok(notificationResource);
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Edit A notification",
            Description = "Edit A notification of the Database.",
            Tags = new[] {"Notifications"})]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveNotificationResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var notification = _mapper.Map<SaveNotificationResource, Notification>(resource);

            var result = await _notificationService.UpdateAsync(id,notification );
            
            if (!result.Success)
                return BadRequest(result.Message);

            var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);

            return Ok(notificationResource);
        }
        
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete A notification",
            Description = "Delete A notification of the Database.",
            Tags = new[] {"Notifications"})]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _notificationService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);
            
            return Ok(notificationResource);
        }
    }
}