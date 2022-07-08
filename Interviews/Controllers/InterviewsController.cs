using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Domain.Services;
using EasyJob.API.Applicants.Resources;
using EasyJob.API.Interviews.Domain.Models;
using EasyJob.API.Interviews.Domain.Services;
using EasyJob.API.Interviews.Resources;
using Go2Climb.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace EasyJob.API.Interviews.Controllers
{
    
    [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class InterviewsController : ControllerBase
    {
        private readonly IInterviewServices _interviewService;
        private readonly IMapper _mapper;

        public InterviewsController(IInterviewServices interviewService, IMapper mapper)
        {
            _interviewService = interviewService;
            _mapper = mapper;
        }
         [HttpGet]
        [SwaggerOperation(
            Summary = "Get All interview",
            Description = "Get All interviews already stored",
            Tags = new[] {"Interviews"})]
        public async Task<IEnumerable<InterviewResources>> GetAllAsync()
        {
            var interviews = await _interviewService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Interview>, IEnumerable<InterviewResources>>(interviews);
            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get interview By Id",
            Description = "Get A interview From The Database Identified By Its Id.",
            Tags = new[] {"Interviews"})]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _interviewService.GetById(id);
        
            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Resource);
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Register A interview",
            Description = "Add A interview to Database.",
            Tags = new[] {"Interviews"})]
        public async Task<IActionResult> PostAsync([FromBody] SaveInterviewResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var interview = _mapper.Map<SaveInterviewResource, Interview>(resource);
            var result = await _interviewService.SaveAsync(interview);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var interviewResources = _mapper.Map<Interview, InterviewResources>(result.Resource);

            return Ok(interviewResources);
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Edit A interview",
            Description = "Edit A interview of the Database.",
            Tags = new[] {"Interviews"})]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveInterviewResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var interview = _mapper.Map<SaveInterviewResource, Interview>(resource);

            var result = await _interviewService.UpdateAsync(id, interview);
            
            if (!result.Success)
                return BadRequest(result.Message);

            var interviewResources = _mapper.Map<Interview, InterviewResources>(result.Resource);

            return Ok(interviewResources);
        }
        
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete A interview",
            Description = "Delete A interview of the Database.",
            Tags = new[] {"Interviews"})]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _interviewService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var interviewResources = _mapper.Map<Interview, InterviewResources>(result.Resource);
            
            return Ok(interviewResources);
        }
        
        
    }
}