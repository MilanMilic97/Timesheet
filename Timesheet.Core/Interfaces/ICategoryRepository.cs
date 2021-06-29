using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities;

namespace Timesheet.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Maybe<Category> GetById(int id);
    }
}
