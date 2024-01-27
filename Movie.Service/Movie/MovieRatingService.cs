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
    public class MovieRatingService : IMovieRatingService
    {
        private readonly IUnitOfWork _uow;
        private readonly ApplicationDbContext _context;

        public MovieRatingService(IUnitOfWork uow, ApplicationDbContext context)
        {
            _uow = uow;
            _context = context;
        }
        public async Task<GenericResponseModel> RateMovie(int movieId, int rating)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    //Check rating value
                    if (rating < 1 || rating > 5)
                        return new GenericResponseModel { StatusCode = 300, StatusMessage = "Rating is on a scale from 1 to 5", Data = false };
                    var movie = await _uow.Movies.Get(movieId);
                    if (movie is null)
                        return new GenericResponseModel { StatusCode = 300, StatusMessage = "Movie not found", Data = false };

                    var newRating = new MovieRating
                    {
                        MovieId = movieId,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        Rating = rating
                    };
                    await _uow.MovieRatings.Add(newRating);
                    await _context.SaveChangesAsync();
                    //Average Rating
                    //Get all movie rating
                    var Ratings = await _uow.MovieRatings.GetSingleMovieRating(movieId);
                    var totalRating = Ratings.Sum(x => x.Rating);
                    var countRating = Ratings.Count();
                    var movieRating = totalRating / countRating;
                    //Update rating
                    movie.Rating = movieRating;
                    await _context.SaveChangesAsync();

                    scope.Complete();
                    return new GenericResponseModel { StatusCode = 200, StatusMessage = "Success", Data = true };
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    ServiceUtil.WriteToFile("Exception in Movie Rating: " + ex.Message + " " + ex.StackTrace + " " + ex.InnerException);
                    return new GenericResponseModel { StatusMessage = ex.Message, StatusCode = 500, Data = false };
                }
            }
        }
    }
}
