using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities;
using Timesheet.Core.Interfaces;

namespace Timesheet.Core.Tests
{
   public class ClientData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var mockedClientRepo = new Mock<IClientRepository>();
            var mocekdUnitOfWork = new Mock<IUnitOfWork>();
            Country country = Country.Create(1, "Serbia").Value;
            Client client1 = Client.Create(1, "Client", "Trive Vitasovica", "Sremska Mitrovica", 22000, country).Value;
            Client client2 = Client.Create(2, "Client2", "Jug Bogdana", "Novi Sad", 21000, country).Value;
            Client client3 = Client.Create(3, "Client3", "Masarinkova 55", "Lacarak", 22221, country).Value;
            Client client4 = Client.Create(4, "Client4", "Kosavska 53", "Lacarak", 22221, country).Value;
            List<Client> clients = new List<Client>();
            clients.Add(client1);
            clients.Add(client2);
            clients.Add(client3);
            clients.Add(client4);


            yield return new object[] {
                Client.Create(5 , "Client 5", "Perina Ulica", "Zrenjanin", 23215, country).Value, mockedClientRepo, mocekdUnitOfWork
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
