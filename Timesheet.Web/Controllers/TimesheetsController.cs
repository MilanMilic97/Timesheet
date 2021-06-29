using CSharpFunctionalExtensions;
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
    public class TimesheetsController : ControllerBase
    {
        private readonly IDailyTimesheetRepository _dailyTimesheetRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProjectRepository _projectCategory;
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly IUnitOfWork _unitOfWork;

        private DailyTimesheetDtoFactory dailyTimesheetDtoFactory = new DailyTimesheetDtoFactory();


        public TimesheetsController(IDailyTimesheetRepository dailyTimesheetRepository, ICategoryRepository categoryRepository, IProjectRepository projectCategory, ITeamMemberRepository teamMemberRepository,IUnitOfWork unitOfWork)
        {
            _dailyTimesheetRepository = dailyTimesheetRepository;
            _categoryRepository = categoryRepository;
            _projectCategory = projectCategory;
            _teamMemberRepository = teamMemberRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Maybe<DailyTimesheet> result = _dailyTimesheetRepository.GetById(id);
            DailyTimesheetDto dailyTimesheetDto = dailyTimesheetDtoFactory.Create(result.Value);
            _unitOfWork.Commit();

            return Ok(dailyTimesheetDto);
        }

        [HttpPost]
        public IActionResult Add(DailyTimesheetDto dailyTimesheetDto)
        {
            Result<DailyTimesheet> result = DailyTimesheet.Create(dailyTimesheetDto.Id, dailyTimesheetDto.Time, dailyTimesheetDto.TimesheetEntries.Select(x => {
                Category category = _categoryRepository.GetById(x.CategoryId).Value;
                Project project = _projectCategory.GetById(x.ProjectId).Value;
                TimesheetEntry timesheetEntry = TimesheetEntry.Create(x.Description, x.Time, x.Overtime, category, project).Value;
                return timesheetEntry;
            }), _teamMemberRepository.GetById(dailyTimesheetDto.TeamMemberId).Value);
            if (result.IsFailure)
            {
                return BadRequest();
            }
            _dailyTimesheetRepository.Insert(result.Value);
            _unitOfWork.Commit();

            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _dailyTimesheetRepository.Remove(id);
            _unitOfWork.Commit();
            return Ok();
        }
    }
}
