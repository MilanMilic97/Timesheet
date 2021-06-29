using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities.ValueObjects;

namespace Timesheet.Core.Entities
{
   public class TimesheetEntry
    {
        public TimesheetDescription Description{ get; }
        public Time Time { get; }
        public Time Overtime { get; }
        public Category Category { get; }
        public Project Project { get; }

        public TimesheetEntry(TimesheetDescription description, Time time, Time overtime, Category category, Project project)
        {
            Description = description;
            Time = time;
            Overtime = overtime;
            Category = category;
            Project = project;
        }
       
        public static Result<TimesheetEntry> Create(string description, double time, double overtime, Category category, Project project)
        {

            Result<TimesheetDescription> descriptionResult = TimesheetDescription.Create(description);
            Result<Time> timeResult = Time.Create(time);
            Result<Time> overtimeResult = Time.Create(overtime);

            Result result = Result.Combine(descriptionResult, timeResult, overtimeResult);
            if (result.IsFailure)
            {
                return Result.Failure<TimesheetEntry>("Timesheet creating failed");
            }
            return Result.Success<TimesheetEntry>(new TimesheetEntry(descriptionResult.Value, timeResult.Value, overtimeResult.Value, category, project));

        }
    }
}
