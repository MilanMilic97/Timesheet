using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities;

namespace Timesheet.Core.Interfaces
{
   public interface ICountryRepository
    {
        Maybe<Country> GetById(int id);
        IEnumerable<Country> GetAll();
    }
}
