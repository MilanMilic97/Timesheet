using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet.Core.Entities.ValueObjects
{
    public class ClientName : ValueObject
    {
        private string Value;

        public static implicit operator string(ClientName clientName) => clientName.Value;
        private ClientName(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() {
            return Value;
        }
        public static Result<ClientName> Create(string value) {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<ClientName>("Client name can't be empty");
            if (value.Length > 50)
                return Result.Failure<ClientName>("Client name can't have more than 50 characters");

            return Result.Success(new ClientName(value));
        }
    }
}
