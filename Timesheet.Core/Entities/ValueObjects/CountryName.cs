using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Entities.ValueObjects
{
    public class CountryName : ValueObject
    {
        private string Value;

        public static implicit operator string(CountryName cn) => cn.Value;

        private CountryName(string value)
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

        public static Result<CountryName> Create(string value)
        {          
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<CountryName>("Country name can't be empty");
            if (value.Length > 50)
                return Result.Failure<CountryName>("Country name can't be over 50 characters");            
            return Result.Success(new CountryName(value));
        }
    }
}
