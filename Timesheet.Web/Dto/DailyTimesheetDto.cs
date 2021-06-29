using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Core.Entities;

namespace Timesheet.Web.Dto
{
    public class DailyTimesheetDto
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public IEnumerable<TimesheetEntryDto> TimesheetEntries { get; set; }
        public int TeamMemberId{ get; set; }

        public DailyTimesheetDto() { }

        public DailyTimesheetDto(int id, DateTime time, IEnumerable<TimesheetEntryDto> timesheetEntries, int teamMemberId)
        {
            Id = id;
            Time = time;
            TimesheetEntries = timesheetEntries;
            TeamMemberId = teamMemberId;
        }
    }

   
}
