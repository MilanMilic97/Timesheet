using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timesheet.Core.Entities;
using Timesheet.Core.Helpers;

namespace Timesheet.Core.Interfaces
{
   public interface IClientRepository
    {
        PagedList<Client> GetAll(PageParameters pageParameters);
        Maybe<Client> GetById(int id);
        void Insert(Client client);
        void Update(Client client);
        void Remove(int id);
        int GetNumberOfClients();


    }
}
