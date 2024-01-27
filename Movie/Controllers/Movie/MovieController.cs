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
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        [Route("addMovie")]
        [Produces(typeof(GenericResponseModel))]
        public async Task<IActionResult> AddMovie([FromForm] MovieDto movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _movieService.AddMovie(movie);
            return Ok(result);
        }

        [HttpGet]
        [Route("movieById")]
        [Produces(typeof(GenericResponseModel))]
        public async Task<IActionResult> GetMovieById(int movieId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _movieService.GetMovieById(movieId);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("allMovie")]
        [Produces(typeof(GenericResponseModel))]
        public async Task<IActionResult> GetAllMovie()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _movieService.GetAllMovie();
            return Ok(result);
        }

        [HttpDelete]
        [Route("deleteMovie")]
        [Produces(typeof(GenericResponseModel))]
        public async Task<IActionResult> DeleteMovie(int movieId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _movieService.DeleteMovie(movieId);
            return Ok(result);
        }
    }
}
