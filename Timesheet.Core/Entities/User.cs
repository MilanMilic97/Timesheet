using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities.ValueObjects;

namespace Timesheet.Core.Entities
{
   public abstract class User
    {
        public int Id { get; set; }
        public EmailAddress Email { get; }
        public HashPassword Password { get; }
        public Username Username { get; }

        protected User(int id, EmailAddress email, HashPassword password, Username username)
        {
            Id = id;
            Email = email;
            Password = password;
            Username = username;
        }
    }
}
