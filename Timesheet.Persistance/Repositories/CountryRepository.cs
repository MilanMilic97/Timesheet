using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Timesheet.Core.Entities;
using Timesheet.Core.Interfaces;

namespace Timesheet.Persistance.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly SqlConnection _connection;

        public CountryRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public Maybe<Country> GetById(int id)
        {
            string query = "SELECT Id, Name FROM[dbo].[Countries] WHERE Id = @Id";
            using SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return Maybe<Country>.None;
            }

           
            return Country.Create(reader.GetInt32(reader.GetOrdinal("Id")), reader.GetString(1)).Value;

        }
    }
}
