using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet.Web.Dto
{
    public class CountryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public CountryDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public CountryDto()
        {
        }
    }
}
