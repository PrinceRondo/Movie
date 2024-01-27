using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Core.Common.Utilities
{
    public class ServiceUtil
    {
        public static void WriteToFile(string Message)
        {
            string path = "C:\\inetpub\\apilogs\\Movielog";

            if (!Directory.Exists(path))
            {

                Directory.CreateDirectory(path);

            }
            string filepath = path + "\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";

            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(DateTime.Now + " :: " + Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(DateTime.Now + " :: " + Message);
                }
            }
        }

        //public static string SafeGetString(SqlDataReader reader, int colIndex)
        //{
        //    if (!reader.IsDBNull(colIndex))
        //        return reader.GetString(colIndex);
        //    return string.Empty;
        //}

        public static bool IsFundSufficient(decimal transamount, decimal walletbalance, decimal BlockedBalance)
        {
            return (walletbalance - BlockedBalance) >= transamount;
        }
    }
}
