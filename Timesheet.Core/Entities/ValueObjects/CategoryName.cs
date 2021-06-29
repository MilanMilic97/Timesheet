using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Entities.ValueObjects
{
   public class CategoryName : ValueObject
    {
        private string Value;

        public static implicit operator string(CategoryName c) => c.Value;
        private CategoryName(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    
        public override string ToString()
        {
            return Value;
        }
        public static Result<CategoryName> Create(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return Result.Failure<CategoryName>("Category name can't be empty");
            }
            if (value.Length > 50)
            {
                return Result.Failure<CategoryName>("Category name can't be over 50 characters");
            }
            return Result.Success(new CategoryName(value));
        }
    }
}
