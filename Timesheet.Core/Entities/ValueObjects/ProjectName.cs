using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Entities.ValueObjects
{
    public class ProjectName : ValueObject
    {
        private string Value;

        public static implicit operator string(ProjectName pn) => pn.Value;

        private ProjectName(string value)
        {
            Value = value;
        }
        public override string ToString()
        {
            return Value;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static Result<ProjectName> Create(string value)
        {     
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<ProjectName>("Project name can't be empty");
            if (value.Length > 50)
                return Result.Failure<ProjectName>("Project name cab't have over 50 characters");          
            return Result.Success(new ProjectName(value));
        }
    }
}
