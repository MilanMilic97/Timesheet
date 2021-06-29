using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet.Web.DTO
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int ZipCode { get; set; }
        public int CountryId { get; set; }

        public ClientDto(int id, string name, string city, string street, int zipCode, int countryId)
        {
            Id = id;
            Name = name;
            City = city;
            Street = street;
            ZipCode = zipCode;
            CountryId = countryId;
        }

        public ClientDto()
        {
        }
    }
}
