using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Core.Entities;
using Timesheet.Web.Dto;

namespace Timesheet.Web.DtoFactories
{
    public class TimesheetEntryDtoFactory
    {
        //Da li je okej staviti da su ovo staticke metode zbog instanciranja i zauzimanja memorije?
        public static TimesheetEntryDto Create(TimesheetEntry timesheetEntry)
        {
            return new TimesheetEntryDto(timesheetEntry.Description, timesheetEntry.Time, timesheetEntry.Overtime, timesheetEntry.Category.Id, timesheetEntry.Project.Id);
        }
    }
}
