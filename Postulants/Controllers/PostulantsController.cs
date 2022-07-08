using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Resources;
using EasyJob.API.Postulants.Domain;
using EasyJob.API.Postulants.Domain.Models;
using EasyJob.API.Postulants.Resources;
using Go2Climb.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EasyJob.API.Postulants.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class PostulantsController : ControllerBase
    {
        private readonly IPostulantService _postulantService;
        private readonly IMapper _mapper;

        public PostulantsController(IPostulantService postulantService, IMapper mapper)
        {
            _postulantService = postulantService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All postulants",
            Description = "Get All postulants already stored",
            Tags = new[] {"Postulants"})]
        public async Task<IEnumerable<PostulantResource>> GetAllAsync()
        {
            var applicants = await _postulantService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Postulant>, IEnumerable<PostulantResource>>(applicants);
            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get postulant By Id",
            Description = "Get A postulant From The Database Identified By Its Id.",
            Tags = new[] {"Postulants"})]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _postulantService.GetById(id);
        
            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Resource);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Register A postulant",
            Description = "Add A postulant to Database.",
            Tags = new[] {"Postulants"})]
        public async Task<IActionResult> PostAsync([FromBody] SavePostulantResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var activity = _mapper.Map<SavePostulantResource, Postulant>(resource);
            var result = await _postulantService.SaveAsync(activity);

            if (!result.Success)
                return BadRequest(result.Message);

            var activityResource = _mapper.Map < Postulant, PostulantResource>(result.Resource);

            return Ok(activityResource);
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Edit A postulant",
            Description = "Edit A postulant of the Database.",
            Tags = new[] {"Postulants"})]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SavePostulantResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var applicant = _mapper.Map<SavePostulantResource, Postulant>(resource);

            var result = await _postulantService.UpdateAsync(id, applicant);
            
            if (!result.Success)
                return BadRequest(result.Message);

            var applicantResource = _mapper.Map<Postulant, PostulantResource>(result.Resource);

            return Ok(applicantResource);
        }
        
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete A postulant",
            Description = "Delete A postulant of the Database.",
            Tags = new[] {"Postulants"})]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _postulantService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var applicantResource = _mapper.Map<Postulant, PostulantResource>(result.Resource);
            
            return Ok(applicantResource);
        }
    }
}