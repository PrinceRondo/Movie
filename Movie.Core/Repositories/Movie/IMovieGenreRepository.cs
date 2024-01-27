﻿using Movie.Core.Common.Enum;
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
    public interface IMovieGenreRepository : IGenericRepository<MovieGenre>
    {
    }
}
