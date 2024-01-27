using Movie.Core.DTOs;
using Movie.Core.DTOs.RequestDtos.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Services.Configuration
{
    public interface ICountryService
    {
        Task<GenericResponseModel> AddCountry(CountryDto country);
        Task<GenericResponseModel> UpdateCountry(int countryId, CountryDto country);
        Task<GenericResponseModel> GetCountryById(int countryId);
        Task<GenericResponseModel> GetAllCountry();
        Task<GenericResponseModel> DeleteCountry(int countryId);
    }
}
