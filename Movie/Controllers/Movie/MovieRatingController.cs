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
using Movie.Core.Services.Movie;
using Movie.Core.DTOs.RequestDtos.Movie;

namespace Movie.Controllers.Movie
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieRatingController : ControllerBase
    {
        private readonly IMovieRatingService _movieRatingService;

        public MovieRatingController(IMovieRatingService movieRatingService)
        {
            _movieRatingService = movieRatingService;
        }

        [HttpPost]
        [Route("rateMovie")]
        [Produces(typeof(GenericResponseModel))]
        public async Task<IActionResult> RateMovie(int movieId, int rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _movieRatingService.RateMovie(movieId, rating);
            return Ok(result);
        }
    }
}
