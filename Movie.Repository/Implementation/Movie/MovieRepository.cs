using Movie.Core.Data;
using Movie.Core.Entities.Configuration;
using Movie.Core.Repositories.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Core.Entities.Movie;
using Movie.Core.Repositories.Movie;
using Movie.Core.DTOs.ResponseDtos;

namespace Movie.Repository.Implementation.Movie
{
    public class MovieRepository : GenericRepository<Movies>, IMovieRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<MovieResponseDto>> GetAllMovie()
        {
            var movies =
                from movie in _context.Movies
                let genre = _context.MovieGenres.Where(x => x.MovieId == movie.Id).ToList()
                select new MovieResponseDto
                {
                    CountryId = movie.CountryId,
                    CountryName = movie.Country.CountryName,
                    CreatedDate = movie.CreatedDate,
                    Description = movie.Description,
                    Id = movie.Id,
                    IsDeleted = movie.IsDeleted,
                    ModifiedDate = movie.ModifiedDate,
                    MovieGenres = genre,
                    Name = movie.Name,
                    Photo = movie.Photo,
                    Rating = movie.Rating,
                    ReleaseDate = movie.ReleaseDate,
                    TicketPrice = movie.TicketPrice
                };

            return movies.AsNoTracking().ToListAsync();
        }

        public async Task<Movies?> GetMovieByName(string name)
        {
            return await _context.Movies.Where(x => x.Name.ToLower() == name.ToLower() && x.IsDeleted == false).FirstOrDefaultAsync();
        }
    }
}
