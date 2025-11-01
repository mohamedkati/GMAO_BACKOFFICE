using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.DIHelpers
{
    internal static class PasswordGenerator
    {
        private static readonly string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
        private static readonly string Numbers = "0123456789";
        private static readonly string SpecialCharacters = "!@#$%^&*()-_=+[]{}|;:,.<>?/";

        public static string GeneratePassword(int length)
        {
            if (length < 8)
            {
                throw new ArgumentException("Password length must be at least 8 characters.");
            }

            Random random = new Random();

            // Ensure the password contains at least one of each required character type
            char[] password = new char[length];
            password[0] = UppercaseLetters[random.Next(UppercaseLetters.Length)];
            password[1] = LowercaseLetters[random.Next(LowercaseLetters.Length)];
            password[2] = Numbers[random.Next(Numbers.Length)];
            password[3] = SpecialCharacters[random.Next(SpecialCharacters.Length)];

            // Fill the remaining characters with a random mix of all character sets
            string allCharacters = UppercaseLetters + LowercaseLetters + Numbers + SpecialCharacters;
            for (int i = 4; i < length; i++)
            {
                password[i] = allCharacters[random.Next(allCharacters.Length)];
            }

            // Shuffle the password to randomize the order
            return new string(password.OrderBy(x => random.Next()).ToArray());
        }
    }
}
