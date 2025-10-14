using System.Text;
using System.Text.RegularExpressions;

namespace BibliotecaUteco.Client.Utilities;

public static class FieldNormalizedHelper
{
    public static string NormalizeField(this string field)
    {
        if (string.IsNullOrWhiteSpace(field))
            return string.Empty;

        var normalizedString = field.Normalize(NormalizationForm.FormD);
        var regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
        var withoutAccents = regex.Replace(normalizedString, string.Empty);
    
        return withoutAccents
            .Normalize(NormalizationForm.FormC)
            .Replace(" ", "")
            .ToLower()
            .Trim();
    }
}