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
    public class MovieGenreRepository : GenericRepository<MovieGenre>, IMovieGenreRepository
    {
        private readonly ApplicationDbContext _context;
        public MovieGenreRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
