using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Entities.ValueObjects
{
   public class Time : ValueObject
    {
        private double Value;

        public static implicit operator double(Time t) => t.Value;

        private Time(double value)
        {
            Value = value;
        }
        public double ToDouble()
        {
            return Value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static Result<Time> Create(double value)
        {
            if (value < 0)
                return Result.Failure<Time>("Hours can't be less than 0");
            return Result.Success(new Time(value));
        }
    }
}
