using Movie.Core.Common.Enum;
using Movie.Core.DTOs.ResponseDtos;
using Movie.Core.Entities;
using Movie.Core.Entities.Configuration;
using Movie.Core.Entities.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Repositories.Movie
{
    public interface IMovieRepository : IGenericRepository<Movies>
    {
        Task<Movies?> GetMovieByName(string name);
        Task<List<MovieResponseDto>> GetAllMovie();
    }
}
