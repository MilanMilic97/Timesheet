using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Entities.ValueObjects
{
   public class HashPassword : ValueObject
    {
        private string Value;

        private HashPassword(string value)
        {
            Value = value;
        }

        public static implicit operator string(HashPassword p) => p.Value;

        public override string ToString()
        {
            return Value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public bool Compare(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, this.Value);
        }

        public static Result<HashPassword> Create(string value)
        {
            if (value.Length != 60)
            {
                return Result.Success(new HashPassword(BCrypt.Net.BCrypt.HashPassword(value)));
            }

            return Result.Success(new HashPassword(value));
                                   
        }

    }
}
