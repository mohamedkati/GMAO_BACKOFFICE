using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Shared.Strings
{
    public static class StringsHelper
    {
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var result = new StringBuilder();
            result.Append(char.ToLowerInvariant(input[0]));

            for (int i = 1; i < input.Length; i++)
            {
                if (char.IsUpper(input[i]))
                {
                    result.Append('_');
                    result.Append(char.ToLowerInvariant(input[i]));
                }
                else
                {
                    result.Append(input[i]);
                }
            }

            return result.ToString();

        }
    }
}
