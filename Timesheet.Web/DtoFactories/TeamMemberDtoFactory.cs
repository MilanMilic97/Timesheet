using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Core.Entities;
using Timesheet.Web.Dto;

namespace Timesheet.Web.DtoFactories
{
    public class TeamMemberDtoFactory
    {
        public static TeamMemberDto Create(TeamMember teamMember)
        {
            return new TeamMemberDto(teamMember.Id, teamMember.Username, teamMember.Password, teamMember.Email, teamMember.Name, teamMember.HourPerWeek, teamMember.Status, teamMember.Role);
        }
       
    }
}
