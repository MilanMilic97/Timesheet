using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Timesheet.Core.Entities;
using Timesheet.Core.Helpers;
using Timesheet.Core.Interfaces;

namespace Timesheet.Persistance.Repositories
{
   public class ClientRepository : IClientRepository
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;
       

        public ClientRepository(SqlConnection conn,SqlTransaction transaction)
        {
            _connection = conn;
            _transaction = transaction;


        }

        public void Insert(Client client)
        {
            string query = "INSERT INTO [dbo].[Clients]([Name],[City],[Street],[ZipCode],[Country_Id])VALUES(@name, @city, @street, @zipCode, @countryId)";
            using SqlCommand command = new SqlCommand(query, _connection, _transaction);
            command.Parameters.AddWithValue("@name", client.Name.ToString());
            command.Parameters.AddWithValue("@city", client.Address.City.ToString());
            command.Parameters.AddWithValue("@street", client.Address.Street.ToString());
            command.Parameters.AddWithValue("@zipCode", client.Address.ZipCode.ToInt());
            command.Parameters.AddWithValue("@countryId", client.Address.Country.Id);
            command.ExecuteNonQuery();
       
        }

        public void Remove(int id)
        {
            string query = "DELETE FROM [dbo].[Clients] WHERE Id = @Id";
            using SqlCommand command = new SqlCommand(query, _connection, _transaction);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }

        public PagedList<Client> GetAll(PageParameters pageParameters)
        {
            string query = "SELECT Clients.Id, Clients.Name, Clients.City, Clients.Street, Clients.ZipCode, Clients.Country_Id, Countries.Name " +
                           "FROM Clients JOIN Countries ON Clients.Country_Id = Countries.Id " +
                           "WHERE Clients.Name like @Letter";
                           
            List<Client> clients = new List<Client>();
            using SqlCommand command = new SqlCommand(query, _connection, _transaction);
            if (string.IsNullOrWhiteSpace(pageParameters.Letter))
                command.Parameters.AddWithValue("@Letter", "%%");
            else
                command.Parameters.AddWithValue("@Letter", pageParameters.Letter + "%");           
           
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
              var client = Client.Create(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), Country.Create(reader.GetInt32(5), reader.GetString(6)).Value).Value;
              clients.Add(client);
            }

            return PagedList<Client>.ToPagedList((clients),pageParameters.PageNumber, pageParameters.PageSize);
        }

        public Maybe<Client> GetById(int id)
        {
            //pitati za ovaj query i query od getAll metode, koji je bolji?
            string query = "SELECT Clients.Id, Clients.Name, Clients.City, dbo.Clients.Street, Clients.ZipCode, Clients.Country_Id, Countries.Name " +
                           "FROM Clients JOIN Countries ON Clients.Country_Id = Countries.Id " +
                           "WHERE Clients.Id = @Id";

            using SqlCommand command = new SqlCommand(query, _connection, _transaction);
            command.Parameters.AddWithValue("@Id", id);

            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return Maybe<Client>.None;
            }

            return Client.Create(reader.GetInt32(0),reader.GetString(1),reader.GetString(2), reader.GetString(3), reader.GetInt32(4),Country.Create(reader.GetInt32(5), reader.GetString(6)).Value).Value;
            
        }

        public void Update(Client client)
        {
            string query = "UPDATE Clients SET Name = @Name, City = @City, Street = @Street, ZipCode = @ZipCode, Country_Id = @CountryId WHERE Id = @Id";
            using SqlCommand command = new SqlCommand(query, _connection, _transaction);
            command.Parameters.AddWithValue("@Name", client.Name.ToString());
            command.Parameters.AddWithValue("@City", client.Address.City.ToString());
            command.Parameters.AddWithValue("@Street", client.Address.Street.ToString());
            command.Parameters.AddWithValue("@ZipCode", client.Address.ZipCode.ToInt());
            command.Parameters.AddWithValue("@CountryId", client.Address.Country.Id);
            command.Parameters.AddWithValue("@Id", client.Id);
            command.ExecuteNonQuery();


        }

        public int GetNumberOfClients()
        {
            string query = "SELECT COUNT(*) FROM Clients";
            using SqlCommand command = new SqlCommand(query, _connection, _transaction);

            using SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                
            }

            return reader.GetInt32(0);
        }
    }
}
