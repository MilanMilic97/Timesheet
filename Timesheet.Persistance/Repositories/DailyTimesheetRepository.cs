using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Timesheet.Core.Entities;
using Timesheet.Core.Entities.Enum;
using Timesheet.Core.Interfaces;
using System.Data;

namespace Timesheet.Persistance.Repositories
{
    public class DailyTimesheetRepository : IDailyTimesheetRepository
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;

        public DailyTimesheetRepository(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public IEnumerable<DailyTimesheet> GetAll()
        {
            throw new NotImplementedException();
        }

        public Maybe<DailyTimesheet> GetById(int id)
        {
            List<TimesheetEntry> timesheetEntries = new List<TimesheetEntry>();

            string query = "SELECT Timesheets.Description, Timesheets.Time, Timesheets.Overtime, Timesheets.DailyTimeSheet_Id, Timesheets.Category_Id, Timesheets.Project_Id, Timesheets.User_Id," + //0-desc, 1-time, 2-OT, 3-DTSID, 4-CatID, 5-ProjId, 6-UserID
                "Categories.Name, Projects.Name, Projects.Description, Projects.Status, Projects.Client_Id, Projects.User_Id," + //7-CatName, 8-projName, 9-ProjDesc, 10-ProjStat,11 ClID, 12-UserID
                "Clients.Name, Clients.Street, Clients.City, Clients.ZipCode, Clients.Country_Id, Countries.Name, " + //13-18
                "Users.Username, Users.Password, Users.Email, Users.Name, Users.HoursPerWeek, Users.Status, Users.Role " + //19-25
                "FROM Timesheets " +
                "JOIN Categories on Timesheets.Category_Id = Categories.Id " +
                "JOIN Projects on Timesheets.Project_Id = Projects.Id " +
                "JOIN Clients on Projects.Client_id = Clients.Id " +
                "JOIN Countries on Clients.Country_Id = Countries.Id " +
                "JOIN Users on Projects.User_Id = Users.Id " +
                "WHERE Timesheets.DailyTimeSheet_Id = @Id";

            using SqlCommand command = new SqlCommand(query, _connection, _transaction);
            command.Parameters.AddWithValue("@Id", id);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var teamMember = TeamMember.Create(reader.GetInt32(12), reader.GetString(19), reader.GetString(20), reader.GetString(21), reader.GetString(22), reader.GetDouble(23), (TeamMemberStatus)reader.GetInt32(24), (Role)reader.GetInt32(25)).Value;
                var client = Client.Create(reader.GetInt32(11), reader.GetString(13), reader.GetString(14), reader.GetString(15), reader.GetInt32(16), Country.Create(reader.GetInt32(17), reader.GetString(18)).Value).Value;
                var project = Project.Create(reader.GetInt32(5), reader.GetString(8), reader.GetString(9), (ProjectStatus)reader.GetInt32(10), teamMember, client).Value;
                var category = Category.Create(reader.GetInt32(4), reader.GetString(7)).Value;
                var timesheetEntry = TimesheetEntry.Create(reader.GetString(0), reader.GetDouble(1), reader.GetDouble(2), category, project).Value;

                timesheetEntries.Add(timesheetEntry);
            }
            reader.Close();

            query = "SELECT DailyTimesheets.Time, DailyTimesheets.User_Id, Users.Username, Users.Password, Users.Email, Users.Name, Users.HoursPerWeek, Users.Status, Users.Role " +
                "FROM DailyTimesheets JOIN Users on DailyTimesheets.User_Id = Users.Id WHERE DailyTimesheets.Id = @Id";
            using SqlCommand command2 = new SqlCommand(query, _connection,_transaction);
            command2.Parameters.AddWithValue("@Id", id);
            using SqlDataReader reader2 = command2.ExecuteReader();
            if (!reader2.Read())
            {
                return Maybe<DailyTimesheet>.None;
            }

            var tm = TeamMember.Create(reader2.GetInt32(1), reader2.GetString(2), reader2.GetString(3), reader2.GetString(4), reader2.GetString(5), reader2.GetDouble(6), (TeamMemberStatus)reader2.GetInt32(7), (Role)reader2.GetInt32(8)).Value;
            Result<DailyTimesheet> dailyTimesheet = DailyTimesheet.Create(id, reader2.GetDateTime(0), timesheetEntries, tm).Value;

            if (dailyTimesheet.IsFailure)
            {
                return Maybe<DailyTimesheet>.None;
            }
            return dailyTimesheet.Value;
        }

        public void Insert(DailyTimesheet dailyTimesheet)
        {
            string query = "INSERT INTO DailyTimesheets (Time, User_Id) VALUES (@Time, @UserId)";
            
            using SqlCommand command = new SqlCommand(query, _connection , _transaction);
            command.Parameters.AddWithValue("@Time", dailyTimesheet.Time);
            command.Parameters.AddWithValue("@UserId", dailyTimesheet.TeamMember.Id);

            command.ExecuteNonQuery();

            query = "INSERT INTO Timesheets (Description, Overtime, Time, DailyTimeSheet_Id, Category_Id, Project_Id, User_Id) VALUES (@Description, @Overtime, @Time, (SELECT Id FROM DailyTimesheets WHERE Time = @Date), @Category_Id, @Project_Id, @User_Id)";
            using SqlCommand command2 = new SqlCommand(query, _connection , _transaction);

            command2.Parameters.Add("@Description", SqlDbType.NVarChar);
            command2.Parameters.Add("@Overtime", SqlDbType.Float);
            command2.Parameters.Add("@Time", SqlDbType.Float);
            command2.Parameters.Add("@Date", SqlDbType.DateTime);
            command2.Parameters.Add("@Category_Id", SqlDbType.Int);
            command2.Parameters.Add("@Project_Id", SqlDbType.Int);
            command2.Parameters.Add("@User_Id", SqlDbType.Int);

            foreach (TimesheetEntry timesheetEntry in dailyTimesheet.TimesheetEntries)
            {              
                command2.Parameters["@Description"].Value = timesheetEntry.Description.ToString();
                command2.Parameters["@Overtime"].Value = timesheetEntry.Overtime.ToDouble();
                command2.Parameters["@Time"].Value = timesheetEntry.Time.ToDouble();
                command2.Parameters["@Date"].Value = dailyTimesheet.Time;
                command2.Parameters["@Category_Id"].Value = timesheetEntry.Category.Id;
                command2.Parameters["@Project_Id"].Value = timesheetEntry.Project.Id;
                command2.Parameters["@User_Id"].Value = dailyTimesheet.TeamMember.Id;
                command2.ExecuteNonQuery();
            }

         
        }

        public void Remove(int id)
        {
            string query = "DELETE FROM Timesheets WHERE DailyTimeSheet_Id = @Id";
            using SqlCommand command = new SqlCommand(query, _connection,_transaction);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();

            query = "DELETE FROM DailyTimesheets WHERE Id = @Id";
            using SqlCommand command2 = new SqlCommand(query, _connection, _transaction);
            command2.Parameters.AddWithValue("@Id", id);
            command2.ExecuteNonQuery();

        }

        public void Update(DailyTimesheet dailyTimesheet)
        {
            throw new NotImplementedException();
        }
    }
}
