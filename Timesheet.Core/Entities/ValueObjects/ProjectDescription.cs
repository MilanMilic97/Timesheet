using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Entities.ValueObjects
{
    public class ProjectDescription : ValueObject
    {
        private string Value;

        public static implicit operator string(ProjectDescription pd) => pd.Value;

        private ProjectDescription(string value)
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

        public static Result<ProjectDescription> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<ProjectDescription>("Description can't be empty");
            if (value.Length > 200)
                return Result.Failure<ProjectDescription>("Description can't have over 200 characters");
            return Result.Success(new ProjectDescription(value));
        }
    }
}
