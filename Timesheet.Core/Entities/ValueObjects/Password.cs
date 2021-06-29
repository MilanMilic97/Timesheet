using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Timesheet.Core.Entities.ValueObjects
{
    public class Password : ValueObject
    {
        private string Value;

        public static implicit operator string(Password p) => p.Value;
        private Password(string value)
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

        public static Result<Password> Create(string value)
        {
           // Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$");
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<Password>("Password can't be empty");           
          //  if (!regex.IsMatch(value))
               // return Result.Failure<Password>("Minimum eight characters, at least one letter, one number and one special character");
            return Result.Success(new Password(value));
        }
    }
}
