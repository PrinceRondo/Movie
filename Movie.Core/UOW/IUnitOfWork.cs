using Movie.Core.Repositories.Configuration;
using Movie.Core.Repositories.DataAudit;
using Movie.Core.Repositories.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IAuditLogRepository AuditLogs { get; }
        #region Configuration
        ICountryRepository Countries { get; }
        #endregion

        #region Movie
        IMovieRepository Movies { get; }
        IMovieGenreRepository MovieGenres { get; }
        IMovieRatingRepository MovieRatings { get; }
        #endregion
        Task CompleteAsync();
        ///// <summary>
        ///// This will be rarely needed for saving to the DB synchronously
        ///// </summary>
        void Complete();
    }
}
