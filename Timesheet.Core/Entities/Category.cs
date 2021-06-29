using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities.ValueObjects;

namespace Timesheet.Core.Entities
{
   public class Category
    {
        public int Id { get; }
        public CategoryName Name { get; }

        private Category(int id, CategoryName name)
        {
            Id = id;
            Name = name;
        }

        public static Result<Category> Create(int id, string name)
        {
            Result<CategoryName> categoryNameResult = CategoryName.Create(name);
            if (categoryNameResult.IsFailure)
            {
                return Result.Failure<Category>("Category creating failed");
            }
            return Result.Success(new Category(id, categoryNameResult.Value));
        }
    }
}
