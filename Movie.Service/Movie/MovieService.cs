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
using Movie.Core.DTOs.RequestDtos.Movie;
using Movie.Core.Entities.Movie;
using Movie.Core.Services.Movie;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Movie.Service.Configuration
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork _uow;
        private readonly ApplicationDbContext _context;

        public MovieService(IUnitOfWork uow, ApplicationDbContext context)
        {
            _uow = uow;
            _context = context;
        }
        public async Task<GenericResponseModel> AddMovie(MovieDto movie)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    //Check if movie exist
                    var record = await _uow.Movies.GetMovieByName(movie.Name);
                    if (record != null)
                        return new GenericResponseModel { StatusCode = 300, StatusMessage = "Movie has already been saved", Data = false };

                    IFormFile file = movie.Photo;
                    //save photo
                    string filename = Guid.NewGuid() + file.FileName.Replace(" ", "_");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\Photo", filename);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var newMovie = new Movies
                    {
                        Name = movie.Name,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        CountryId = movie.CountryId,
                        Description = movie.Description,
                        Photo = filename,
                        Rating = 0,
                        ReleaseDate = movie.ReleaseDate,
                        TicketPrice = movie.TicketPrice
                    };
                    await _uow.Movies.Add(newMovie);
                    await _context.SaveChangesAsync();
                    //Add genre
                    foreach (var genre in movie.MovieGenres)
                    {
                        var newGenre = new MovieGenre
                        {
                            CreatedDate = DateTime.Now,
                            Genre = genre,
                            IsDeleted = false,
                            ModifiedDate = DateTime.Now,
                            MovieId = newMovie.Id
                        };
                        await _uow.MovieGenres.Add(newGenre);
                    }
                    await _context.SaveChangesAsync();
                    scope.Complete();
                    return new GenericResponseModel { StatusCode = 200, StatusMessage = "Success", Data = true };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    ServiceUtil.WriteToFile("Exception in Movie Creation: " + ex.Message + " " + ex.StackTrace + " " + ex.InnerException);
                    return new GenericResponseModel { StatusMessage = ex.Message, StatusCode = 500, Data = false };
                }
            }
        }

        public async Task<GenericResponseModel> DeleteMovie(int movieId)
        {
            try
            {
                var record = await _uow.Movies.Get(movieId);
                if (record == null)
                    return new GenericResponseModel { StatusCode = 404, StatusMessage = "Record not found", Data = false };

                _uow.Movies.Delete(record);
                await _uow.CompleteAsync();
                return new GenericResponseModel { StatusCode = 200, StatusMessage = "Success", Data = true };
            }
            catch (Exception ex)
            {
                ServiceUtil.WriteToFile("Exception in Deleting Movie: " + ex.Message + " " + ex.StackTrace + " " + ex.InnerException);
                return new GenericResponseModel { StatusMessage = ex.Message, StatusCode = 500, Data = false };
            }
        }

        public async Task<GenericResponseModel> GetAllMovie()
        {
            var record = await _uow.Movies.GetAllMovie();
            if (!record.Any())
                return new GenericResponseModel { StatusCode = 404, StatusMessage = "No record found", Data = null };

            return new GenericResponseModel { StatusCode = 200, StatusMessage = "Success", Data = record };
        }

        public async Task<GenericResponseModel> GetMovieById(int movieId)
        {
            var record = await _uow.Movies.Get(movieId);
            if (record == null)
                return new GenericResponseModel { StatusCode = 404, StatusMessage = "Record not found", Data = null };

            return new GenericResponseModel { StatusCode = 200, StatusMessage = "Success", Data = record };
        }
    }
}
