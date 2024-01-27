using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core.Common.Utilities
{
  public class RandomNumberGenerator
  {
    //For generating random numbers for Email Confirmaton Codes
    public string randomCodesGen()
    {
      try
      {
        Random rnd = new Random();
        int codes = rnd.Next(1000, 9999);

        return codes.ToString();
      }
      catch (Exception)
      {
        throw;
      }

    }

    //For generating the cart referenceId
    private static Random random = new Random();
    public static string RandomString(int length = 10)
    {
      try
      {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
      }
      catch (Exception)
      {
        throw;
      }
    }

    //For generating a Unique name for Files uploaded to the server
    public static string UniqueFileName(int length = 27)
    {
      try
      {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
      }
      catch (Exception)
      {
        throw;
      }
    }

    //For generating random numbers for Phone Number Confirmation Codes
    public string randomCodesGenMobile()
    {
      try
      {
        Random rnd = new Random();
        int codes = rnd.Next(1000, 9999);

        return codes.ToString();
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
