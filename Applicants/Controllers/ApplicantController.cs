using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Domain.Services;
using EasyJob.API.Applicants.Resources;
using Go2Climb.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EasyJob.API.Applicants.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        private readonly IMapper _mapper;

        public ApplicantController(IApplicantService applicantService, IMapper mapper)
        {
            _applicantService = applicantService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All applicants",
            Description = "Get All applicants already stored",
            Tags = new[] {"Applicants"})]
        public async Task<IEnumerable<ApplicantResource>> GetAllAsync()
        {
            var applicants = await _applicantService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Applicant>, IEnumerable<ApplicantResource>>(applicants);
            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get Applicant By Id",
            Description = "Get A applicant From The Database Identified By Its Id.",
            Tags = new[] {"Applicants"})]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _applicantService.GetById(id);
        
            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Resource);
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Register A applicant",
            Description = "Add A applicant to Database.",
            Tags = new[] {"Applicants"})]
        public async Task<IActionResult> PostAsync([FromBody] SaveApplicantResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var activity = _mapper.Map<SaveApplicantResource, Applicant>(resource);
            var result = await _applicantService.SaveAsync(activity);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var activityResource = _mapper.Map<Applicant, ApplicantResource>(result.Resource);

            return Ok(activityResource);
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Edit A applicant",
            Description = "Edit A applicant of the Database.",
            Tags = new[] {"Applicants"})]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveApplicantResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var applicant = _mapper.Map<SaveApplicantResource, Applicant>(resource);

            var result = await _applicantService.UpdateAsync(id, applicant);
            
            if (!result.Success)
                return BadRequest(result.Message);

            var applicantResource = _mapper.Map<Applicant, ApplicantResource>(result.Resource);

            return Ok(applicantResource);
        }
        
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete A applicant",
            Description = "Delete A applicant of the Database.",
            Tags = new[] {"Applicants"})]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _applicantService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var applicantResource = _mapper.Map<Applicant, ApplicantResource>(result.Resource);
            
            return Ok(applicantResource);
        }
    }
}