using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities.Enum;
using Timesheet.Core.Entities.ValueObjects;

namespace Timesheet.Core.Entities
{
   public class TeamMember : User
    {
        public TeamMemberName Name { get; }
        public HoursPerWeek HourPerWeek { get; }
        public TeamMemberStatus Status{ get; }
        public Role Role{ get; }

        protected TeamMember(Username username, int id, EmailAddress email, HashPassword password, TeamMemberName name, HoursPerWeek hourPerWeek, TeamMemberStatus status, Role role) : base(id, email, password, username)
        {
            Name = name;
            HourPerWeek = hourPerWeek;
            Status = status;
            Role = role;
        }

        public static Result<TeamMember> Create(int id, string username, string password, string emailAddress, string name, double hoursPerWeek, TeamMemberStatus status, Role role)
        {
            Result<TeamMemberName> teamMemberNameResult = TeamMemberName.Create(name);
            Result<HoursPerWeek> hoursPerWeekResult = HoursPerWeek.Create(hoursPerWeek);
            Result<Username> userNameResult = Username.Create(username);
            Result<HashPassword> passwordResult = HashPassword.Create(password);
            Result<EmailAddress> emailAddressResult = EmailAddress.Create(emailAddress);
            Result result = Result.Combine(teamMemberNameResult, hoursPerWeekResult, userNameResult, passwordResult, emailAddressResult);
            if (result.IsFailure)
            {
                return Result.Failure<TeamMember>("Team member data not acceptable");

            }

            return Result.Success(new TeamMember(userNameResult.Value, id, emailAddressResult.Value, passwordResult.Value, teamMemberNameResult.Value, hoursPerWeekResult.Value, status, role));
        }
    }
}
