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

namespace Movie.Repository.Implementation.Movie
{
    public class MovieRatingRepository : GenericRepository<MovieRating>, IMovieRatingRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieRatingRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<MovieRating>?> GetSingleMovieRating(int movieId)
        {
            return await _context.MovieRatings.Where(x => x.MovieId == movieId).ToListAsync();
        }
    }
}
