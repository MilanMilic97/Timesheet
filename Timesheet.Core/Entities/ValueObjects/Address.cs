using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities.ValueObjects;

namespace Timesheet.Core.Entities
{
    public class Address : ValueObject
    {
        public City City { get; }
        public Street Street { get; }
        public ZipCode ZipCode { get;  }
        public Country Country{ get; }
        public Address(City city, Street street, ZipCode zipCode, Country country)
        {
            City = city;
            Street = street;
            ZipCode = zipCode;
            Country = country;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return City;
            yield return Street;           
            yield return ZipCode;
            yield return Country;
        }
        public static Result<Address> Create(string street, string city, int zipCode, Country country)
        {
            Result<City> cityResult = City.Create(city);
            Result<Street> streetResult = Street.Create(street);
            Result<ZipCode> zipCodeResult = ZipCode.Create(zipCode);
            Result result = Result.Combine(streetResult, cityResult, zipCodeResult);
            if (result.IsFailure)
            {
                return Result.Failure<Address>("Address creating failed");
            }
            return Result.Success(new Address(cityResult.Value, streetResult.Value, zipCodeResult.Value, country));
        }
    }
}
