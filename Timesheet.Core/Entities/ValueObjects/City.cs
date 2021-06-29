using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Entities.ValueObjects
{
    public class City : ValueObject
    {
        private string Value;

        public static implicit operator string(City c) => c.Value;

        private City(string value)
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

        public static Result<City> Create(string value)
        {           
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<City>("City name can't be empty");
            if (value.Length > 50)
                return Result.Failure<City>("City name can't have over 50 characters");         
            return Result.Success(new City(value));
        }
    }
}
