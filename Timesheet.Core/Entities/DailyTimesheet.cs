using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Entities
{
   public class DailyTimesheet
    {
        public int Id { get; }
        public DateTime Time { get; }
        public IEnumerable<TimesheetEntry> TimesheetEntries { get;  }
        public TeamMember TeamMember { get; }

        private DailyTimesheet(int id, DateTime time, IEnumerable<TimesheetEntry> timesheetEntries, TeamMember teamMember)
        {
            Id = id;
            Time = time;
            TimesheetEntries = timesheetEntries;
            TeamMember = teamMember;
        }

        public static Result<DailyTimesheet> Create(int id, DateTime date, IEnumerable<TimesheetEntry> timesheetEntries, TeamMember teamMember)
        {
            return Result.Success(new DailyTimesheet(id, date, timesheetEntries, teamMember));
        }
    }
}
