using Movie.Core.Data;
using Movie.Core.Repositories.Configuration;
using Movie.Core.Repositories.DataAudit;
using Movie.Core.Repositories.Movie;
using Movie.Core.UOW;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Repository.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IAuditLogRepository AuditLogs { get; }
        public IMovieRepository Movies { get; }
        public IMovieGenreRepository MovieGenres { get; }
        public IMovieRatingRepository MovieRatings { get; }
        public ICountryRepository Countries { get; }

        public UnitOfWork(ApplicationDbContext dbContext,IMovieRepository movieRepository,IAuditLogRepository auditLogRepository,
            ICountryRepository countryRepository, IMovieGenreRepository movieGenreRepository, IMovieRatingRepository movieRatingRepository)
        {
            _context = dbContext;

            AuditLogs = auditLogRepository;
            Countries = countryRepository;
            Movies = movieRepository;
            MovieGenres = movieGenreRepository;
            MovieRatings = movieRatingRepository;
        }
        public async Task CompleteAsync()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    AuditLogs.SetUpAuditTrail();
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (DbEntityValidationException devex)
                {
                    var outputLines = new StringBuilder();

                    foreach (var eve in devex.EntityValidationErrors)
                    {
                        outputLines.AppendLine(
                            $"{DateTime.Now}: Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors: ");

                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.AppendLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }

                    transaction.Rollback();

                    throw new DbEntityValidationException(outputLines.ToString(), devex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void Complete()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    AuditLogs.SetUpAuditTrail();
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (DbEntityValidationException devex)
                {
                    var outputLines = new StringBuilder();

                    foreach (var eve in devex.EntityValidationErrors)
                    {
                        outputLines.AppendLine(
                            $"{DateTime.Now}: Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors: ");

                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.AppendLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }

                    transaction.Rollback();

                    throw new DbEntityValidationException(outputLines.ToString(), devex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
