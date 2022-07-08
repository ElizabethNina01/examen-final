using System.Collections.Generic;
using System.Threading.Tasks;
using Go2Climb.API.Extensions;
using AutoMapper;
using EasyJob.API.Announcements.Domain.Models;
using EasyJob.API.Announcements.Domain.Services;
using EasyJob.API.Announcements.Resources;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace EasyJob.API.Announcements.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/[controller]")]
    
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;
        private readonly IMapper _mapper;

        public AnnouncementController(IMapper mapper, IAnnouncementService applicantService)
        {
            _mapper = mapper;
            _announcementService = applicantService;
        }
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All Announcement",
            Description = "Get All Announcement already stored",
            Tags = new[] {"Announcements"})]
        public async Task<IEnumerable<AnnouncementResource>> GetAllAsync()
        {
            var applicants = await  _announcementService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Announcement>, IEnumerable<AnnouncementResource>>(applicants);
            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get Applicant By Id",
            Description = "Get A applicant From The Database Identified By Its Id.",
            Tags = new[] {"Announcements"})]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _announcementService.GetById(id);
        
            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Resource);
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Register A Announcement",
            Description = "Add A Announcement to Database.",
            Tags = new[] {"Announcements"})]
        public async Task<IActionResult> PostAsync([FromBody] SaveAnnouncementResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var activity = _mapper.Map<SaveAnnouncementResource, Announcement>(resource);
            var result = await  _announcementService.SaveAsync(activity);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var activityResource = _mapper.Map<Announcement, AnnouncementResource>(result.Resource);

            return Ok(activityResource);
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Edit A Announcement",
            Description = "Edit A Announcement of the Database.",
            Tags = new[] {"Announcements"})]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveAnnouncementResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var applicant = _mapper.Map<SaveAnnouncementResource, Announcement>(resource);

            var result = await  _announcementService.UpdateAsync(id, applicant);
            
            if (!result.Success)
                return BadRequest(result.Message);

            var applicantResource = _mapper.Map<Announcement, AnnouncementResource>(result.Resource);

            return Ok(applicantResource);
        }
        
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete A Announcement",
            Description = "Delete A Announcement of the Database.",
            Tags = new[] {"Announcements"})]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await  _announcementService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var applicantResource = _mapper.Map<Announcement, AnnouncementResource>(result.Resource);
            
            return Ok(applicantResource);
        }
        
        
        
        
        
        
        
    }
}