using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Common.Enums
{
    public enum SmsType
    {
        /// <summary>
        /// SMS is deliver to the text message inbox on the users phone
        /// </summary>
        Normal,

        /// <summary>
        /// SMS is displayed on user's screen. The user decides to either save or not
        /// </summary>
        Flash
    }
}
