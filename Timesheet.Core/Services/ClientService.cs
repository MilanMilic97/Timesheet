using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Core.Entities;
using Timesheet.Core.Interfaces;

namespace Timesheet.Core.Services
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;        
        }

        public Result Insert(Client client)
        {
            
            if (_clientRepository.GetById(client.Id).HasValue)
            {
                return Result.Failure($"Client with ID-{client.Id} already exists");
            }
            _clientRepository.Insert(client);
            _unitOfWork.Commit();
            return Result.Success();
        }

        public Result Delete(int id)
        {
            if (_clientRepository.GetById(id).HasNoValue)
            {
                return Result.Failure($"Client with ID-{id} doesn't exists");
            }
            _clientRepository.Remove(id);
            _unitOfWork.Commit();
            return Result.Success();
        }
        public Result Update(Client client)
        {
            if (_clientRepository.GetById(client.Id).HasNoValue)
            {
                return Result.Failure($"Client with ID-{client.Id} doesn't exists");
            }
            _clientRepository.Update(client);
            _unitOfWork.Commit();
            return Result.Success();
        }
    }
}
