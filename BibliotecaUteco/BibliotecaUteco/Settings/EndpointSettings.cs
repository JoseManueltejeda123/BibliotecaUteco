namespace BibliotecaUteco.Settings;

public static class EndpointSettings
{
    private static string ApiV1 => "v1";
    public static string UsersEndpoint => $"api/{ApiV1}/users";
}