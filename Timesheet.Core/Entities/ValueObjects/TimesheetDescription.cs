using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Entities.ValueObjects
{
   public class TimesheetDescription : ValueObject
    {
        private string Value;

        public static implicit operator string(TimesheetDescription td) => td.Value;

        private TimesheetDescription(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        public override string ToString()
        {
            return Value;
        }
        public static Result<TimesheetDescription> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<TimesheetDescription>("Description can't be empty");
            if (value.Length > 200)
                return Result.Failure<TimesheetDescription>("Description can't have over 200 characters");
            return Result.Success(new TimesheetDescription(value));
        }
    }
}
