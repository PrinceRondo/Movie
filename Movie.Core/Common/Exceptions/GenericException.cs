using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Common.Exceptions
{
    public class MovieGenericException : Exception
    {
        public string ErrorCode { get; set; }

        public MovieGenericException(string message) : base(message)
        { }



        public MovieGenericException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }

    public class CustomException : Exception
    {

        public string ResponseCode { get; set; }
        public CustomException()
        {

        }
        public CustomException(string message, string responseCode = "400") : this(message)
        {
            ResponseCode = responseCode;
        }

        public CustomException(string message)
            : base(message)
        { }

        public CustomException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
