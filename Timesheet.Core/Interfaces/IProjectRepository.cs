using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities;

namespace Timesheet.Core.Interfaces
{
   public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
        Maybe<Project> GetById(int id);
        void Insert(Project project);
        void Update(Project project);
        void Remove(int id);
    }
}
