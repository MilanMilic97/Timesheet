using CSharpFunctionalExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Timesheet.Core.Entities;
using Timesheet.Core.Interfaces;
using Timesheet.Core.Services;
using Xunit;
using Assert = Xunit.Assert;

namespace Timesheet.Core.Tests
{
    [TestClass]
    public class ClientUnitTests
    {
        
        [Theory]
        [ClassData(typeof(ClientData))]
        public void DeleteFailIfClientDoesntExists (Client client, Mock<IClientRepository> repository, Mock<IUnitOfWork> unitOfWork)
        {
            ClientService clientService = new ClientService(repository.Object, unitOfWork.Object);

            Result result = clientService.Delete(client.Id);

            Assert.True(result.IsFailure);

        }

        [Theory]
        [ClassData(typeof(ClientData))]
        public void CreateClient(Client client, Mock<IClientRepository> repository, Mock<IUnitOfWork> unitOfWork)
        {
            ClientService clientService = new ClientService(repository.Object, unitOfWork.Object);

            Result result = clientService.Insert(client);

            Assert.True(result.IsSuccess);
        }

    }
}
