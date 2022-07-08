using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EasyJob.API.Applicants.Domain.Models;
using EasyJob.API.Applicants.Resources;
using EasyJob.API.Projects.Domain.Models;
using EasyJob.API.Projects.Domain.Services;
using EasyJob.API.Projects.Resources;
using Go2Climb.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EasyJob.API.Projects.Controllers
{   [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/[controller]")]
  
    public class ProjectsController: ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }
        
        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get All Projects",
            Description = "Get All Projects already stored",
            Tags = new[] {"Projects"})]
        public async Task<IEnumerable<ProjectResource>> GetAllAsync()
        {
            var projects = await _projectService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Project>, IEnumerable<ProjectResource>>(projects);
            return resources;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get Project By Id",
            Description = "Get A Project From The Database Identified By Its Id.",
            Tags = new[] {"Projects"})]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _projectService.GetById(id);
        
            if (!result.Success)
                return BadRequest(result.Message);
            
            return Ok(result.Resource);
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Register A project",
            Description = "Add A project to Database.",
            Tags = new[] {"Projects"})]
        public async Task<IActionResult> PostAsync([FromBody] SaveProjectResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var project = _mapper.Map<SaveProjectResource, Project>(resource);
            var result = await _projectService.SaveAsync(project);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var projectResource = _mapper.Map<Project, ProjectResource>(result.Resource);

            return Ok(projectResource);
        }
        
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Edit A project",
            Description = "Edit A Project of the Database.",
            Tags = new[] {"Projects"})]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveProjectResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            var project = _mapper.Map<SaveProjectResource, Project>(resource);

            var result = await _projectService.UpdateAsync(id, project);
            
            if (!result.Success)
                return BadRequest(result.Message);

            var projectResource = _mapper.Map<Project, ProjectResource>(result.Resource);

            return Ok(projectResource);
        }
        
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete A project",
            Description = "Delete A project of the Database.",
            Tags = new[] {"Projects"})]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _projectService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var projectResource = _mapper.Map<Project, ProjectResource>(result.Resource);
            
            return Ok(projectResource);
        }
        
        
        
        
    }
}