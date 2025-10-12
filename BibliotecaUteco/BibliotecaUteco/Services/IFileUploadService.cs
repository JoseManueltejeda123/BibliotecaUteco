namespace BibliotecaUteco.Services
{
    public interface IFileUploadService
    {
        Task<(bool, string)> UploadImageAsync(IFormFile file, EnvFolders folder, string fileName);
        (bool, string) DeleteFile(string relativePath, EnvFolders folder);
    }

}
    
   


