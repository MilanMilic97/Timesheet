using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Entities.ValueObjects
{
   public class TeamMemberName : ValueObject
    {
        private string Value;

        public static implicit operator string(TeamMemberName tm) => tm.Value;
        private TeamMemberName(string value)
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
        public static Result<TeamMemberName> Create(string value)
        {
            
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<TeamMemberName>("Username can't be empty");
            if (value.Length > 50)
                return Result.Failure<TeamMemberName>("Username can't have more than 50 characters");
            return Result.Success(new TeamMemberName(value));
        }
    }
}
