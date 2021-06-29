using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Timesheet.Core.Entities;
using Timesheet.Core.Interfaces;

namespace Timesheet.Persistance.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlConnection _connection;

        public CategoryRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public Maybe<Category> GetById(int id)
        {
            //pitati za ovaj query i query od getAll metode, koji je bolji?
            string query = "SELECT Categories.Id, Categories.Name " +
                           "FROM Categories " +
                           "WHERE Categories.Id = @Id";

            using SqlCommand command = new SqlCommand(query, _connection);
            command.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return Maybe<Category>.None;
            }

            return Category.Create(reader.GetInt32(0), reader.GetString(1)).Value;

        }
    }
}
