using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Core.Entities;
using Timesheet.Core.Helpers;
using Timesheet.Core.Interfaces;
using Timesheet.Core.Services;
using Timesheet.Web.DTO;
using Timesheet.Web.DtoFactories;

namespace Timesheet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("MyPolicy")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ClientService _clientService;

        private ClientDtoFactory clientDtoFactory = new ClientDtoFactory();

        public ClientsController(IClientRepository clientRepository, ICountryRepository countryRepository, ClientService clientService)
        {
            _clientRepository = clientRepository;
            _countryRepository = countryRepository;
            _clientService = clientService;
        }

        [HttpPost]
        public IActionResult CreateClient(ClientDto clientDto)
        {           
            Maybe<Country> country = _countryRepository.GetById(clientDto.CountryId);
            if (country.HasNoValue)
            {
                return BadRequest();
            }

            Result<Client> result = Client.Create(clientDto.Id, clientDto.Name, clientDto.Street, clientDto.City, clientDto.ZipCode, country.Value);
            if (_clientService.Insert(result.Value).IsFailure)
            {
                return BadRequest();
            }
            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient (int id)
        {
            if (_clientService.Delete(id).IsFailure)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("{id}")]       
        public IActionResult GetClientById(int id)
        {
            Maybe<Client> result = _clientRepository.GetById(id);
            if (result.HasNoValue)
            {
                return BadRequest();
            }

            return Ok(clientDtoFactory.Create(result.Value,0));
        }

        [HttpGet]
        public IActionResult GetAllClients([FromQuery] PageParameters pageParameters)
        {
            PagedList<Client> dsa = _clientRepository.GetAll(pageParameters);
            int a = dsa.TotalPages;
            return Ok(dsa.Select(x => clientDtoFactory.Create(x,a)));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, ClientDto clientDto)
        {
            Maybe<Country> country = _countryRepository.GetById(clientDto.CountryId);
            if (country.HasNoValue)
            {
                return BadRequest();
            }
            Result<Client> result = Client.Create(id, clientDto.Name, clientDto.Street, clientDto.City, clientDto.ZipCode, country.Value);

            if (_clientService.Update(result.Value).IsFailure)
            {
                return BadRequest();
            }

            return Ok();
        }


        [HttpGet]
        [Route("getNumber")]
        public IActionResult GetNumberOfClients()
        {
            return Ok(_clientRepository.GetNumberOfClients());
        }
    }
}
