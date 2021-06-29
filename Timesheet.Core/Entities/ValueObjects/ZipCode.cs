using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Entities.ValueObjects
{
   public class ZipCode : ValueObject
    {
        private int Value;

        public static implicit operator int(ZipCode zc) => zc.Value;

        private ZipCode(int value)
        {
            Value = value;
        }
      
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public int ToInt()
        {
            return Value;
        }
        public static Result<ZipCode> Create(int value)
        {
            if (value < 0)
                return Result.Failure<ZipCode>("ZipCode can't be less than 0");
            return Result.Success(new ZipCode(value));
        }
    }
}
