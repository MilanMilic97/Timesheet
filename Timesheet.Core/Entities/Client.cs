using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities.ValueObjects;

namespace Timesheet.Core.Entities
{
    public class Client
    {
        public int Id { get; }
        public ClientName Name { get; }
        public Address Address { get; }

        private Client(int id, ClientName name, Address address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        private Client()
        {
        }

        public static Result<Client> Create (int id, string name, string street, string city, int zipCode, Country country) {
            Result<ClientName> clientNameResult = ClientName.Create(name);
            Result<Address> addressResult = Address.Create(street, city, zipCode, country);
            Result result = Result.Combine(clientNameResult, addressResult);
            if (result.IsFailure)
            {
                return Result.Failure<Client>("Client name not acceptable");
                
            }

            return Result.Success(new Client(id, clientNameResult.Value, addressResult.Value));
        }
    }
}
