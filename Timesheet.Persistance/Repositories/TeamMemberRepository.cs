using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Timesheet.Core.Entities;
using Timesheet.Core.Entities.Enum;
using Timesheet.Core.Interfaces;

namespace Timesheet.Persistance.Repositories
{
    public class TeamMemberRepository : ITeamMemberRepository
    {
        private SqlConnection _connection;

        public TeamMemberRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public Maybe<TeamMember> FindByUsername(string username)
        {
            string query = "SELECT Users.Id, Users.Username, Users.Password, Users.Email, Users.Name, Users.HoursPerWeek, Users.Status, Users.Role FROM Users WHERE Users.Username = @Username";
            using SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Username", username);
            command.ExecuteNonQuery();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (!reader.Read())
                {
                    return Maybe<TeamMember>.None;
                }
                TeamMember teamMember = TeamMember.Create(
                        reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),
                        reader.GetDouble(5), (TeamMemberStatus)reader.GetInt32(6),
                        (Role)reader.GetInt32(7)).Value;
                return teamMember;
            }
        }

        public IEnumerable<TeamMember> GetAll()
        {
            throw new NotImplementedException();
        }

        public Maybe<TeamMember> GetById(int id)
        {
            string query = "SELECT Users.Id, Users.Email, Users.Username, Users.Password, Users.Name, " +
                "Users.HoursPerWeek, Users.Status, Users.Role " +
                "from dbo.Users where dbo.Users.id = @id";
            using SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@id", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (!reader.Read())
                {
                    return Maybe<TeamMember>.None;
                }
                TeamMember teamMember = TeamMember.Create(
                        reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),
                        reader.GetDouble(5), (TeamMemberStatus)reader.GetInt32(6),
                        (Role)reader.GetInt32(7)).Value;
                return teamMember;
            }
        }

        public void Insert(TeamMember teamMember)
        {
            string query = "INSERT INTO Users (Username, Password, Email, Name, HoursPerWeek, Status, Role) " +
                "VALUES (@Username, @Password, @Email, @Name, @HoursPerWeek, @Status, @Role)";
            using SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Username", teamMember.Username.ToString());
            command.Parameters.AddWithValue("@Password", teamMember.Password.ToString());
            command.Parameters.AddWithValue("@Email", teamMember.Email.ToString());
            command.Parameters.AddWithValue("@Name", teamMember.Name.ToString());
            command.Parameters.AddWithValue("@HoursPerWeek", teamMember.HourPerWeek.ToDouble());
            command.Parameters.AddWithValue("@Status", teamMember.Status);
            command.Parameters.AddWithValue("@Role", teamMember.Role);
            command.ExecuteNonQuery();
        }

        public void Login()
        {

        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TeamMember teamMember)
        {
            throw new NotImplementedException();
        }
    }
}
