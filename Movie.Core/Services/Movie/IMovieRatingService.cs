using Movie.Core.DTOs;
using Movie.Core.DTOs.RequestDtos.Configuration;
using Movie.Core.DTOs.RequestDtos.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Services.Movie
{
    public interface IMovieRatingService
    {
        Task<GenericResponseModel> RateMovie(int movieId, int rating);
    }
}
