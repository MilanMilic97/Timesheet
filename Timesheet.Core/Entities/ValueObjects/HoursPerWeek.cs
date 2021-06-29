using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Entities.ValueObjects
{
    public class HoursPerWeek : ValueObject
    {
        private double Value;

        public static implicit operator double(HoursPerWeek hpw) => hpw.Value;

        private HoursPerWeek(double value)
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

        public static Result<HoursPerWeek> Create(double value)
        {
            if (value < 0)
                return Result.Failure<HoursPerWeek>("Hours can't be less than 0");
            return Result.Success(new HoursPerWeek(value));
        }
    }
}
