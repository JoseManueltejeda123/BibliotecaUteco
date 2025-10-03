namespace BibliotecaUteco.Features;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}

public static class ApplicationContentTypes
{

    public static string ApplicationJson => "application/json";
    public static string MultipartForm => "multipart/form-data";

}