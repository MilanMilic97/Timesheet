using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Core.Entities;
using Timesheet.Core.Interfaces;
using Timesheet.Web.Dto;
using Timesheet.Web.DtoFactories;

namespace Timesheet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly IAuthentication _authentication;
        private readonly IUnitOfWork _unitOfWork;

        public TeamMemberController(ITeamMemberRepository teamMemberRepository, IAuthentication authentication, IUnitOfWork unitOfWork)
        {
            _teamMemberRepository = teamMemberRepository;
            _authentication = authentication;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult Register(TeamMemberDto teamMemberDto)
        {
            var teamMember = TeamMember.Create(teamMemberDto.Id, teamMemberDto.Username, teamMemberDto.Password, teamMemberDto.Email, teamMemberDto.Name, teamMemberDto.HoursPerWeek, teamMemberDto.Status, teamMemberDto.Role);
            if (teamMember.IsFailure)
            {
                return BadRequest();
            }
            _teamMemberRepository.Insert(teamMember.Value);
            _unitOfWork.Commit();
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            Maybe<TeamMember> teamMember = _teamMemberRepository.FindByUsername(loginRequest.Username);
            if (teamMember.HasNoValue)
            {
                return BadRequest();
            }

            bool compared = teamMember.Value.Password.Compare(loginRequest.Password);

            if (!compared)
            {
                return BadRequest("Incorrect password");
            }

            var loginResponse = new LoginResponse(teamMember.Value, _authentication.Authenticate(teamMember.Value));
            _unitOfWork.Commit();
            return Ok(loginResponse);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
             return Ok(TeamMemberDtoFactory.Create(_teamMemberRepository.GetById(id).Value));
        }
    }
}
