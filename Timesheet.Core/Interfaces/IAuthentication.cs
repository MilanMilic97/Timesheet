using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities;

namespace Timesheet.Core.Interfaces
{
   public interface IAuthentication
    {
        public string Authenticate(TeamMember teamMember);
        
    }
}
