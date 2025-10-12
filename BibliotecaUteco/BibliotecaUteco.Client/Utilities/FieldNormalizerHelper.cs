namespace BibliotecaUteco.Client.Utilities;

public static class FieldNormalizedHelper
{
    public static string NormalizeField(this string field)
    {
        return field.Replace(" ", "").ToLower().Normalize().Trim();
    }
}