using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Core.Entities;
using Timesheet.Web.DTO;

namespace Timesheet.Web.DtoFactories
{
    public class ClientDtoFactory
    {
        public ClientDto Create(Client client, int pageNumber)
        {
            return new ClientDto(client.Id, client.Name, client.Address.City, client.Address.Street, client.Address.ZipCode, client.Address.Country.Id, pageNumber);
        }
    }
}
