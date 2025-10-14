using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliotecaUteco.Client.Settings;

namespace BibliotecaUteco.Services
{

    public class FileUploadService(IWebHostEnvironment env) : IFileUploadService
    {
        public async Task<(bool, string)> UploadImageAsync(IFormFile file, EnvFolders folder, string fileName)
        {
            if (!IsValidImage(file))
            {
                return (false, "La imagen debe de ser .jpg, .png, .webp, .jpge");
            }

            return await SaveFileAsync(file, folder, fileName);
        }
        
        public (bool, string) DeleteFile(string relativePath, EnvFolders folder)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return (false, "La ruta del archivo no puede estar vacía.");

            try
            {
                var fullPath = Path.Combine(env.WebRootPath, folder.ToString(), Path.GetFileName(relativePath));

                if (!File.Exists(fullPath))
                    return (false, "El archivo no existe.");

                File.Delete(fullPath);

                return (true, "Archivo eliminado correctamente.");
            }
            catch (Exception ex)
            {
                // Podrías registrar el error aquí con tu logger
                return (false, $"Error al eliminar el archivo: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        private async Task<(bool, string)> SaveFileAsync(IFormFile file, EnvFolders folder, string fileName)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return (false, "El archivo subido no tiene contenido");

                if (file.Length > FilesSettings.MaxFileSize)
                {
                    return (false, "El tamaño maximo es 2mb");

                }




                var uploadsPath = Path.Combine(env.WebRootPath, folder.ToString());
                if (!Directory.Exists(uploadsPath))
                    Directory.CreateDirectory(uploadsPath);
                var extension = Path.GetExtension(file.FileName); 
                var uniqueFileName = $"{fileName}{extension}";
                var filePath = Path.Combine(uploadsPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var relativePath = Path.Combine("/", folder.ToString(), uniqueFileName).Replace("\\", "/");
                return (true, relativePath);
            }
            catch(Exception ex)
            {
                return (false, $"Error al subir el archivo: {ex.InnerException?.Message ?? ex.Message}");
            }
           
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
        BookCovers,
        UserPictures
    }

}
    
   


