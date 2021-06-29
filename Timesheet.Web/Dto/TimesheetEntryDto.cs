using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet.Web.Dto
{
    public class TimesheetEntryDto
    {
        public string Description { get; set; }
        public double Time { get; set; }
        public double Overtime { get; set; }
        public int CategoryId { get; set; }
        public int ProjectId { get; set; }

        public TimesheetEntryDto() { }

        public TimesheetEntryDto(string description, double time, double overtime, int categoryId, int projectId)
        {
           
            Description = description;
            Time = time;
            Overtime = overtime;
            CategoryId = categoryId;
            ProjectId = projectId;
        }
    }
}
