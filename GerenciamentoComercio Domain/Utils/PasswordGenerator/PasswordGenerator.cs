using System;

namespace GerenciamentoComercio_Domain.Utils.PasswordGenerator
{
    public class PasswordGenerator
    {
        public static string GerarSenha(int tamanho = 15)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$&*?_";
            Random random = new Random();

            char[] chars = new char[tamanho];
            for (int i = 0; i < tamanho; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public static bool ValidatePassword(string pwd, int minLength = 6, int minUpper = 1, int minLower = 0, int minNumbers = 0, int minSpecial = 0, int minLetters = 0)
        {

            // Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters.
            var upper = new System.Text.RegularExpressions.Regex("[A-Z]");
            var lower = new System.Text.RegularExpressions.Regex("[a-z]");
            var number = new System.Text.RegularExpressions.Regex("[0-9]");
            // Special is "none of the above".
            var special = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]");

            // Check the length.
            if (pwd.Length < minLength)
                return false;
            // Check for minimum number of occurrences.
            if (upper.Matches(pwd).Count < minUpper)
                return false;
            if (lower.Matches(pwd).Count < minLower)
                return false;
            if (number.Matches(pwd).Count < minNumbers)
                return false;
            if (special.Matches(pwd).Count < minSpecial)
                return false;
            if (upper.Matches(pwd).Count < minLetters & lower.Matches(pwd).Count < minLetters)
                return false;

            // Passed all checks.
            return true;
        }

    }
}
