using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities;

namespace Timesheet.Core.Interfaces
{
   public interface ITeamMemberRepository
    {
        IEnumerable<TeamMember> GetAll();
        Maybe<TeamMember> GetById(int id);
        void Insert(TeamMember teamMember);
        void Update(TeamMember teamMember);
        void Remove(int id);

        Maybe<TeamMember> FindByUsername(string username);


    }
}
