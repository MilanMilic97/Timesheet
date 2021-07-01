using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet.Core.Interfaces;
using Timesheet.Web.Dto;

namespace Timesheet.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]

    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

       
        public CountriesController(ICountryRepository countryRepository)
        {
          
           _countryRepository = countryRepository;
        }

        [HttpGet]      
        public IActionResult GetCountries()
        {
            return Ok(_countryRepository.GetAll().Select(item => new CountryDto(item.Id, item.Name)));
        }


    }
}
