using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities.ValueObjects;

namespace Timesheet.Core.Entities
{
    public class Country
    {
        public int Id { get; }
        public CountryName Name { get; }

        private Country(int id, CountryName name)
        {
            Id = id;
            Name = name;
        }
        public static Result<Country> Create (int id, string name)
        {
            Result<CountryName> countryNameResult = CountryName.Create(name);
            if (countryNameResult.IsFailure)
            {
                return Result.Failure<Country>("Country creating failed");
            }
            return Result.Success(new Country(id, countryNameResult.Value));
        }

    }
}
