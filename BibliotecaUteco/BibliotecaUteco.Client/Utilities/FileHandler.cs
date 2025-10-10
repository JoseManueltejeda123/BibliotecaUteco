using Microsoft.AspNetCore.Components.Forms;

namespace BibliotecaUteco.Client.Utilities;

public static class FileHandler
{
    private const long MaxFileSize =  2 * 1024 * 1024;
    private static readonly List<string> ImageExtensions = new()
    {
        ".png", ".jpg", ".jpeg", ".webp"
    };
    
    public static async Task<(IBrowserFile?, string)> HandleImageFromInputAsync(InputFileChangeEventArgs e)
    {
        try
        {
            var uploadedFile = e.File; 
             if (uploadedFile.Size > MaxFileSize)
            {
                return (null, "El tamaño maximo es 2mb");
                
            }

            if (!IsValidImage(e.File))
            {
                return (null, "La imagen debe de ser .png, .jpg, .jpeg, .webp");
            }
            
            using var stream = uploadedFile.OpenReadStream(maxAllowedSize: 2_000_000);
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            var bytes = ms.ToArray();
            return (uploadedFile, $"data:{uploadedFile.ContentType};base64,{Convert.ToBase64String(bytes)}");
        }
        catch (Exception ex)
        {
            return (null, ex.InnerException?.Message ?? ex.Message);
        }

       
    }
    
    private static bool IsValidImage(IBrowserFile file)
    {
        var ext = Path.GetExtension(file.Name).ToLowerInvariant();
        return ImageExtensions.Contains(ext);
    }
    
    
}