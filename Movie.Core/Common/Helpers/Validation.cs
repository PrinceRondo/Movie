using Movie.Core.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Common.Helpers
{
    public class Validation
    {
        public static void ValidatePin(string pin)
        {
            if (string.IsNullOrWhiteSpace(pin))
                throw new CustomException("Pin is required", "400");


            if (pin.Length < 4 || pin.Length > 4)
                throw new CustomException("Pin length is 4", "400");

            if (!pin.ToCharArray().All(x => char.IsDigit(x)))
                throw new CustomException("Pin can only be digit", "400");
        }

        //public static string GenerateHash(string pin)
        //{
        //    return BCrypt.Net.BCrypt.HashPassword(pin);
        //}
    }
}
