namespace BibliotecaUteco.Services
{
    public interface IFileUploadService
    {
        Task<(bool, string)> SaveFileAsync(IFormFile file, EnvFolders folder, string fileName);
        Task<(bool, string)> UploadImageAsync(IFormFile file, EnvFolders folder, string fileName);
    }

}
    
   


