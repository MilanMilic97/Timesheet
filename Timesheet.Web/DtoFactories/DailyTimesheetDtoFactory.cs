using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Core.Entities;
using Timesheet.Web.Dto;

namespace Timesheet.Web.DtoFactories
{
    public class DailyTimesheetDtoFactory
    {
        //staticka metoda?
        //private TeamMemberDtoFactory teamMemberDtoFactory = new TeamMemberDtoFactory();
        public DailyTimesheetDto Create(DailyTimesheet dailyTimesheet)
        {
            return new DailyTimesheetDto(dailyTimesheet.Id, dailyTimesheet.Time, dailyTimesheet.TimesheetEntries.Select(item => TimesheetEntryDtoFactory.Create(item)), dailyTimesheet.TeamMember.Id);
        }
    }
}
