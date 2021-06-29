using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Interfaces
{
   public interface IUnitOfWork
    {
        public void Commit();
        
    }
}
