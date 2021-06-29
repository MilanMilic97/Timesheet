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
    public class ProjectRepository : IProjectRepository
    {
        private readonly SqlConnection _connection;

        public ProjectRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Project> GetAll()
        {
            throw new NotImplementedException();
        }

        public Maybe<Project> GetById(int id)
        {
            string query = "SELECT Projects.Id, Projects.Name, Projects.Description, Projects.Status, Projects.Client_Id, Projects.User_Id, " +
               "Users.Email, Users.Username, Users.Password, Users.Name, Users.HoursPerWeek, Users.Status, Users.Role, " +
               "Clients.Name, Clients.City, Clients.Street, Clients.ZipCode, Clients.Country_Id, " +
               "Countries.Name " +
               "FROM Projects JOIN Users ON Projects.User_Id = Users.Id " +
               "JOIN Clients ON Projects.Client_Id = Clients.Id " +
               "JOIN Countries ON Clients.Country_Id = Countries.Id " +
               "WHERE Projects.Id = @Id";
            using SqlCommand command = new SqlCommand(query, _connection);

            command.Parameters.AddWithValue("@Id", id);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (!reader.Read())
                {
                    return Maybe<Project>.None;
                }
                TeamMember teamMember = TeamMember.Create(
                        reader.GetInt32(5), reader.GetString(7), reader.GetString(8), reader.GetString(6), reader.GetString(9),
                        reader.GetDouble(10), (TeamMemberStatus)reader.GetInt32(11),
                        (Role)reader.GetInt32(12)).Value;

                Client client = Client.Create(
                        reader.GetInt32(4), reader.GetString(13), reader.GetString(15), reader.GetString(14), reader.GetInt32(16),
                        Country.Create(reader.GetInt32(17), reader.GetString(18)).Value).Value;

                Project project = Project.Create(
                        reader.GetInt32(0), reader.GetString(1), reader.GetString(2), (ProjectStatus)reader.GetInt32(3), teamMember, client).Value;
                return project;
            }
        }


        public void Insert(Project project)
        {
            string query = "INSERT INTO Projects (Name, Description, Status, Client_Id, User_Id) VALUES (@Name, @Description, @Status, @UserId, @ClientId)";
            using SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Name", project.Name.ToString());
            command.Parameters.AddWithValue("@Description", project.Description.ToString());
            command.Parameters.AddWithValue("@Status", project.Status);
            command.Parameters.AddWithValue("@UserId", project.TeamMember.Id);
            command.Parameters.AddWithValue("@ClientId", project.Client.Id);
            command.ExecuteNonQuery();

        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
