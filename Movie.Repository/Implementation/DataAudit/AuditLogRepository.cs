using Movie.Core.Data;
using Movie.Core.Entities.DataAudit;
using Movie.Core.Repositories;
using Movie.Core.Repositories.DataAudit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movie.Repository.Implementation.DataAudit
{
    public class AuditLogRepository : GenericRepository<AuditLog>, IAuditLogRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration configuration;

        public AuditLogRepository(ApplicationDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            this.configuration = configuration;
        }

        public int ExecuteSql(string rawSql, params object[] para)
        {
            throw new NotImplementedException();
        }

        public void SetUpAuditTrail()
        {
            var saveInAuditLogTable = Convert.ToBoolean(configuration["Setiing:SavingInAuditLogTableEnabled"] ?? "true");

            if (!saveInAuditLogTable)
            {
                return;
            }

            var dbEntries = _context.ChangeTracker.Entries()
                                                 .Where(e => ((e.State == EntityState.Added ||
                                                               e.State == EntityState.Modified ||
                                                               e.State == EntityState.Deleted) && e.Entity.GetType().Name.ToLower() != "auditlogs")).ToList();


            if (!dbEntries.Any())
            {
                return;
            }

            var auditLogs = new List<AuditLog>();

            foreach (var entity in dbEntries)
            {
                var oldValues = new StringBuilder();
                var changedValues = new StringBuilder();

                var audit = new AuditLog
                {
                    EntityName = entity.Entity.GetType().Name,
                    ChangeDate = DateTime.Now,
                    ChangeType = entity.State.ToString()
                };


                switch (entity.State)
                {
                    case EntityState.Added:
                        oldValues.Append("Did not exist");

                        foreach (var prop in entity.CurrentValues.Properties)
                        {
                            changedValues.Append(string.Format("{0}: {1} {2}", prop, entity.CurrentValues[prop], Environment.NewLine));
                        }

                        break;

                    case EntityState.Deleted:
                        changedValues.Append("Deleted");

                        foreach (var prop in entity.OriginalValues.Properties)
                        {
                            oldValues.Append(string.Format("{0}: {1} {2}", prop, entity.OriginalValues[prop], Environment.NewLine));
                        }

                        break;

                    case EntityState.Modified:

                        foreach (var prop in entity.CurrentValues.Properties)
                        {
                            oldValues.Append(string.Format("{0}: {1} {2}", prop, entity.OriginalValues[prop], "\n"));

                            //Skip if both are null
                            if (entity.OriginalValues[prop] == null && entity.CurrentValues[prop] == null)
                            {
                                continue;
                            }

                            //Check for differences and save
                            if ( (entity.OriginalValues[prop] == null && entity.CurrentValues[prop] != null) || (entity.OriginalValues[prop] != null && entity.CurrentValues[prop] == null) || (!entity.CurrentValues[prop].Equals(entity.OriginalValues[prop])) )
                            {
                                changedValues.Append(string.Format("{0}: {1} {2}", prop, entity.CurrentValues[prop], Environment.NewLine));
                            }
                        }
                        break;
                }

                audit.OriginalValues = oldValues.ToString();
                audit.ChangedValues = changedValues.ToString();

                auditLogs.Add(audit);

            }

            AddRange(auditLogs);
        }

        //public IQueryable<TEntity> SqlQuery<TEntity>(string rawSql, params object[] para) where TEntity : class
        //{
        //    throw new NotImplementedException();
        //}

        //Task<PagedList<AuditLog>> IRepositoryReadOnly<AuditLog>.GetPagedAsync(int page, int size)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<PagedList<AuditLog>> IRepositoryReadOnly<AuditLog>.GetPagedAsync(Expression<Func<AuditLog, bool>> predicate, int page, int size)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
