using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities;

namespace Timesheet.Core.Interfaces
{
   public interface IDailyTimesheetRepository
    {
        IEnumerable<DailyTimesheet> GetAll();
        Maybe<DailyTimesheet> GetById(int id);
        void Insert(DailyTimesheet dailyTimesheet);
        void Update(DailyTimesheet dailyTimesheet);
        void Remove(int id);
    }
}
