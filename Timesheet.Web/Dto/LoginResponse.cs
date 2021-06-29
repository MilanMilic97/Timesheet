using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Core.Entities;

namespace Timesheet.Web.Dto
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public LoginResponse() { }

        public LoginResponse(TeamMember teamMember, string token)
        {
            Id = teamMember.Id;
            Name = teamMember.Name;
            Username = teamMember.Username;
            Token = token;
        }
    }
}
