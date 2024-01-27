using Movie.Core.Repositories.Configuration;
using Movie.Core.Repositories.DataAudit;
using Movie.Core.Services.Configuration;
using Movie.Core.UOW;
using Movie.Repository.Implementation.Configuration;
using Movie.Repository.Implementation.DataAudit;
using Movie.Repository.UOW;
using Movie.Service.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Core.Repositories.Movie;
using Movie.Repository.Implementation.Movie;
using Movie.Core.Services.Movie;

namespace Movie
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            //Service
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IMovieRatingService, MovieRatingService>();

            //Repo
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAuditLogRepository, AuditLogRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IMovieRatingRepository, MovieRatingRepository>();
            services.AddTransient<IMovieGenreRepository, MovieGenreRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            return services;
        }
    }
}
