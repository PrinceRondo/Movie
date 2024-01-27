using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Common.Enum
{
    public enum PaymentStatus
    {
        Paid = 1,
        Pending,
        Declined,
        Successful,
        Unavailable_BankCode,
        Failed,
        No_Response, Processing, In_Progress
    }
}
