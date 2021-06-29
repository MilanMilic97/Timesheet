using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Timesheet.Core.Entities.ValueObjects
{
   public class EmailAddress : ValueObject    
    {
        private string Value;

        public static implicit operator string(EmailAddress e) => e.Value;

        private EmailAddress(string value)
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

        public static Result<EmailAddress> Create(string value)
        {
          //  Regex regex = new Regex(@"^(?=.{1,64}@)[A-Za-z0-9_-]+(\\.[A-Za-z0-9_-]+)*@[^-][A-Za-z0-9-]+(\\.[A-Za-z0-9-]+)*(\\.[A-Za-z]{2,})$");
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<EmailAddress>("Email can't be empty");
            if (value.Length > 50)
                return Result.Failure<EmailAddress>("Email can't be more than 50 characters");
          //  if (!regex.IsMatch(value))
              //  return Result.Failure<EmailAddress>("Incorrect email format");
            return Result.Success(new EmailAddress(value));
        }
    }
}
