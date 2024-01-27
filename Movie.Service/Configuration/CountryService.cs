using Movie.Core.Common.Utilities;
using Movie.Core.Data;
using Movie.Core.DTOs;
using Movie.Core.DTOs.RequestDtos.Configuration;
using Movie.Core.Entities.Configuration;
using Movie.Core.Services.Configuration;
using Movie.Core.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Service.Configuration
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _uow;

        public CountryService(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<GenericResponseModel> AddCountry(CountryDto country)
        {
            try
            {
                var record = await _uow.Countries.GetCountryByName(country.CountryName);
                if (record != null)
                    return new GenericResponseModel { StatusCode = 300, StatusMessage = "Country has already been saved", Data = false };

                var newCountry = new Country
                {
                    CountryName = country.CountryName,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                };
                await _uow.Countries.Add(newCountry);
                await _uow.CompleteAsync();
                return new GenericResponseModel { StatusCode = 200, StatusMessage = "Success", Data = true };
            }
            catch (Exception ex)
            {
                ServiceUtil.WriteToFile("Exception in Country Creation: " + ex.Message + " " + ex.StackTrace + " " + ex.InnerException);
                return new GenericResponseModel { StatusMessage = ex.Message, StatusCode = 500, Data = false };
            }
        }

        public async Task<GenericResponseModel> DeleteCountry(int countryId)
        {
            try
            {
                var record = await _uow.Countries.Get(countryId);
                if (record == null)
                    return new GenericResponseModel { StatusCode = 404, StatusMessage = "Record not found", Data = false };

                _uow.Countries.Delete(record);
                await _uow.CompleteAsync();
                return new GenericResponseModel { StatusCode = 200, StatusMessage = "Success", Data = true };
            }
            catch (Exception ex)
            {
                ServiceUtil.WriteToFile("Exception in Deleting Country: " + ex.Message + " " + ex.StackTrace + " " + ex.InnerException);
                return new GenericResponseModel { StatusMessage = ex.Message, StatusCode = 500, Data = false };
            }
        }

        public async Task<GenericResponseModel> GetAllCountry()
        {
            var record = await _uow.Countries.GetAll();
            if (!record.Any())
                return new GenericResponseModel { StatusCode = 404, StatusMessage = "No record found", Data = null };

            return new GenericResponseModel { StatusCode = 200, StatusMessage = "Success", Data = record };
        }

        public async Task<GenericResponseModel> GetCountryById(int countryId)
        {
            var record = await _uow.Countries.Get(countryId);
            if (record == null)
                return new GenericResponseModel { StatusCode = 404, StatusMessage = "Record not found", Data = null };

            return new GenericResponseModel { StatusCode = 200, StatusMessage = "Success", Data = record };
        }

        public async Task<GenericResponseModel> UpdateCountry(int countryId, CountryDto country)
        {
            try
            {
                var record = await _uow.Countries.Get(countryId);
                if (record == null)
                    return new GenericResponseModel { StatusCode = 404, StatusMessage = "Record not found", Data = false };

                record.CountryName = country.CountryName;
                record.ModifiedDate = DateTime.Now;
                await _uow.CompleteAsync();
                return new GenericResponseModel { StatusCode = 200, StatusMessage = "Success", Data = true };
            }
            catch (Exception ex)
            {
                ServiceUtil.WriteToFile("Exception in Country Update: " + ex.Message + " " + ex.StackTrace + " " + ex.InnerException);
                return new GenericResponseModel { StatusMessage = ex.Message, StatusCode = 500, Data = false };
            }
        }
    }
}
