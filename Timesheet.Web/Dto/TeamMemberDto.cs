using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Core.Entities.Enum;

namespace Timesheet.Web.Dto
{
    public class TeamMemberDto
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
     
        public double HoursPerWeek { get; set; }

        public TeamMemberStatus Status { get; set; }

        public Role Role { get; set; }

        public TeamMemberDto() { }

        public TeamMemberDto(int id, string username, string password, string email, string name, double hoursPerWeek, TeamMemberStatus status, Role role)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            Name = name;          
            HoursPerWeek = hoursPerWeek;
            Status = status;
            Role = role;
        }
    }
}
