using System.Text.RegularExpressions;

namespace GMAO.API.Configurations
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value == null) return null;

            var stringValue = value.ToString()!;

            // Ne transformer que si c'est un nom de contrôleur
            var str = Regex.Replace(
           value.ToString()!,
           "([a-z])([A-Z])",
           "$1-$2",
           RegexOptions.CultureInvariant,
           TimeSpan.FromMilliseconds(100))
           .ToLowerInvariant();
            return str;
            //return stringValue.ToLowerInvariant();
        }
    }
}
