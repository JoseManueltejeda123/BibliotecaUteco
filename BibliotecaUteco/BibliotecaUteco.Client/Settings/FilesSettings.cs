namespace BibliotecaUteco.Client.Settings
{
    public static class FilesSettings
    {
        public static long MaxFileSize => 2 * 1024 * 1024;
        public static List<string> AllowedImageExtensions => new()
        {
            ".png", ".jpg", ".jpeg", ".webp"

        };

        public static List<string> AllowedImageExtensionsForUpload => new()
        {
            "image/jpeg",
            "image/png",
            "image/jpg",
            "image/webp"
        };
    }
}