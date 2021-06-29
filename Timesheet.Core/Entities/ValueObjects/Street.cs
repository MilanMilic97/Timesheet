using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Entities.ValueObjects
{
   public class Street : ValueObject
    {
        private string Value;

        public static implicit operator string(Street s) => s.Value;

        private Street(string value)
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

        public static Result<Street> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<Street>("Street name can't be empty");
            if (value.Length > 50)
                return Result.Failure<Street>("Street name can't have over 50 characters");
            return Result.Success(new Street(value));
        }
    }
}
