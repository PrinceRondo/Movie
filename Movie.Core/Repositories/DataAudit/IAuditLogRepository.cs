using Movie.Core.Entities.DataAudit;
using Movie.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Repositories.DataAudit
{
    public interface IAuditLogRepository : IGenericRepository<AuditLog>
    {
        void SetUpAuditTrail();
    }
}
