using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Core.Entities;
using Timesheet.Core.Interfaces;
using Timesheet.Web.Dto;

namespace Timesheet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly IUnitOfWork _unitOfWork;


        public ProjectsController(IProjectRepository projectRepository, IClientRepository clientRepository, ITeamMemberRepository teamMemberRepository, IUnitOfWork unitOfWork)
        {
            _projectRepository = projectRepository;
            _clientRepository = clientRepository;
            _teamMemberRepository = teamMemberRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult CreateProject(ProjectDto projectDto)
        {
            Maybe<Client> client = _clientRepository.GetById(projectDto.ClientId);
            Maybe<TeamMember> teamMember = _teamMemberRepository.GetById(projectDto.TeamMemberId);
            if (client.HasNoValue && teamMember.HasNoValue)
            {
                return BadRequest();
            }
            Result<Project> result = Project.Create(projectDto.Id, projectDto.Name, projectDto.Description, projectDto.Status, teamMember.Value, client.Value);
            if (result.IsFailure)
            {
                return BadRequest();
            }
            _projectRepository.Insert(result.Value);
            _unitOfWork.Commit();
            return Ok();
        }
    }
}
