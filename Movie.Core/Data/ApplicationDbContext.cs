﻿using Movie.Core.Data;
using Movie.Core.Entities.Configuration;
using Movie.Core.Entities.DataAudit;
using Movie.Core.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movie.Core.Entities.Movie;

namespace Movie.Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {
        }
        #region Configuration
        public DbSet<Country> Countries { get; set; }
        #endregion
        #region Movie
        public DbSet<Movies> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }
        #endregion

        public DbSet<AuditLog> AuditLogs { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data source=.;Database=Movie;Trusted_Connection=True;MultipleActiveResultSets=True");
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));


                foreach (var property in properties)
                {
                    builder.Entity(entityType.Name).Property(property.Name).HasColumnType("decimal(18,2)");
                }
                base.OnModelCreating(builder);

            }
        }
        public override int SaveChanges()
        {
            PerformEntityAudit();
            return base.SaveChanges();
        }

        //public override Task<int> SaveChangesAsync()
        //{
        //    PerformEntityAudit();
        //    return base.SaveChangesAsync();
        //}

        private void PerformEntityAudit()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        var currentDateTime = DateTime.Now;
                        entry.Entity.DateCreated = currentDateTime;
                        entry.Entity.DateModified = currentDateTime;
                        entry.Entity.IsDeleted = false;
                        break;

                    case EntityState.Modified:
                        entry.Entity.DateModified = DateTime.Now;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;

                        entry.Entity.DateModified = DateTime.Now;
                        entry.Entity.IsDeleted = true;
                        break;
                }
            }
        }
    }
}
