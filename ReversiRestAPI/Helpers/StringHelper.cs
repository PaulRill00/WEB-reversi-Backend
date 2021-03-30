using System;
using System.Linq;

namespace ReversiRestAPI.Models
{
    public class StringHelper
    {

        public static string GenerateRandomString(int length, bool allowNumbers = true)
        {
            Random random = new Random();
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            if (allowNumbers)
                chars += "0123456789";

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
