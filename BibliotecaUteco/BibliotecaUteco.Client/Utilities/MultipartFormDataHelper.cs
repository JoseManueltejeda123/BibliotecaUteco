using System.Net.Http.Headers;
using System.Reflection;
using BibliotecaUteco.Client.Settings;
using Microsoft.AspNetCore.Components.Forms;

namespace BibliotecaUteco.Client.Utilities;

public static class MultipartFormDataHelper
{
    /// <summary>
    /// Convierte un objeto en MultipartFormDataContent automáticamente
    /// </summary>
    public static MultipartFormDataContent ToMultipartFormData(
        this object obj,
        long? maxFileSize = null
    )
    {
        var form = new MultipartFormDataContent();
        var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in properties)
        {
            var value = prop.GetValue(obj);
            if (value is null)
                continue;

            var fieldName = ToCamelCase(prop.Name);

            // Si es un archivo (IBrowserFile)
            if (value is IBrowserFile file)
            {
                form.AddFile(file, fieldName, maxFileSize);
            }
            // Si es una lista de archivos
            else if (value is IEnumerable<IBrowserFile> files)
            {
                form.AddFiles(files, fieldName, maxFileSize);
            }
            // Si es un valor primitivo o string
            else
            {
                form.AddValue(fieldName, value);
            }
        }

        return form;
    }

    /// <summary>
    /// Agrega un valor simple al formulario
    /// </summary>
    public static MultipartFormDataContent AddValue(
        this MultipartFormDataContent form,
        string name,
        object? value
    )
    {
        if (value is not null)
        {
            form.Add(new StringContent(value.ToString()!), name);
        }
        return form;
    }

    /// <summary>
    /// Agrega un archivo al formulario
    /// </summary>
    public static MultipartFormDataContent AddFile(
        this MultipartFormDataContent form,
        IBrowserFile? file,
        string fieldName,
        long? maxFileSize = null
    )
    {
        if (file is null)
            return form;

        var stream = file.OpenReadStream(maxAllowedSize: maxFileSize ?? FilesSettings.MaxFileSize);
        var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
        form.Add(fileContent, fieldName, file.Name);

        return form;
    }

    /// <summary>
    /// Agrega múltiples archivos con el mismo nombre de campo
    /// </summary>
    public static MultipartFormDataContent AddFiles(
        this MultipartFormDataContent form,
        IEnumerable<IBrowserFile> files,
        string fieldName,
        long? maxFileSize = null
    )
    {
        foreach (var file in files)
        {
            form.AddFile(file, fieldName, maxFileSize ?? FilesSettings.MaxFileSize);
        }
        return form;
    }

    /// <summary>
    /// Convierte PascalCase a camelCase
    /// </summary>
    private static string ToCamelCase(string str)
    {
        if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
            return str;

        return char.ToLowerInvariant(str[0]) + str.Substring(1);
    }
}
