using Movie.Core.DTOs;
using Movie.Core.DTOs.RequestDtos.Configuration;
using Movie.Core.Entities.Configuration;
using Movie.Core.Services.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Controllers.Configuration
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpPost]
        [Route("addCountry")]
        [Produces(typeof(GenericResponseModel))]
        public async Task<IActionResult> AddCountry(CountryDto country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _countryService.AddCountry(country);
            return Ok(result);
        }

        [HttpPut]
        [Route("updateCountry")]
        [Produces(typeof(GenericResponseModel))]
        public async Task<IActionResult> UpdateCountry(int countryId, CountryDto country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _countryService.UpdateCountry(countryId, country);
            return Ok(result);
        }

        [HttpGet]
        [Route("countryById")]
        [Produces(typeof(GenericResponseModel))]
        public async Task<IActionResult> GetCountryById(int countryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _countryService.GetCountryById(countryId);
            return Ok(result);
        }
        [HttpGet]
        [Route("allCountry")]
        [Produces(typeof(GenericResponseModel))]
        public async Task<IActionResult> GetAllCountry()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _countryService.GetAllCountry();
            return Ok(result);
        }

        [HttpDelete]
        [Route("deleteCountry")]
        [Produces(typeof(GenericResponseModel))]
        public async Task<IActionResult> DeleteCountry(int countryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _countryService.DeleteCountry(countryId);
            return Ok(result);
        }
    }
}
