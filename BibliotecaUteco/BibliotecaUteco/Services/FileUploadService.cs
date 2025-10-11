using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaUteco.Client.Settings;

namespace BibliotecaUteco.Services
{

    public class FileUploadService(IWebHostEnvironment env) : IFileUploadService
    {
        private readonly IWebHostEnvironment _env = env;

        public async Task<(bool, string)> UploadImageAsync(IFormFile file, EnvFolders folder, string fileName)
        {
            if (!IsValidImage(file))
            {
                return (false, "La imagen debe de ser .jpg, .png, .webp, .jpge");
            }

            return await SaveFileAsync(file, folder, fileName);
        }

        public async Task<(bool, string)> SaveFileAsync(IFormFile file, EnvFolders folder, string fileName)
        {
            if (file == null || file.Length == 0)
                return (false, "El archivo subido no tiene contenido");

            if (file.Length > FilesSettings.MaxFileSize)
            {
                return (false, "El tamaño maximo es 2mb");

            }




            var uploadsPath = Path.Combine(_env.WebRootPath, folder.ToString());
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);
            var extension = Path.GetExtension(file.FileName); 
            var uniqueFileName = $"{folder.ToString()}_{fileName}{extension}";
            var filePath = Path.Combine(uploadsPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Retorna la URL pública
            var relativePath = Path.Combine("/", folder.ToString(), uniqueFileName).Replace("\\", "/");
            return (true, relativePath);
        }

        private static bool IsValidImage(IFormFile file)
        {
            if (file == null || string.IsNullOrWhiteSpace(file.ContentType))
                return false;

            return FilesSettings.AllowedImageExtensionsForUpload.Contains(file.ContentType.ToLower());


        }
    }

    public enum EnvFolders
    {
        BookCovers
    }

}
    
   


