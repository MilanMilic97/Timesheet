using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Entities.ValueObjects
{
   public class Username : ValueObject
    {
        private string Value;

        public static implicit operator string(Username u) => u.Value;

        private Username(string value)
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

        public static Result<Username> Create(string value)
        {
           
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<Username>("Username can't be empty");
            if (value.Length > 50)
                return Result.Failure<Username>("Username can't be more than 50 characters");          
            return Result.Success(new Username(value));
        }
    }
}
