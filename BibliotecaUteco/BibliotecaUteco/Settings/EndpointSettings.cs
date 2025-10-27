namespace BibliotecaUteco.Settings;

public static class EndpointSettings
{
    private static string ApiV1 => "v1";
    public static string UsersEndpoint => $"api/{ApiV1}/users";
    public static string AuthorsEndpoint => $"api/{ApiV1}/authors";
    public static string GenresEndpoint => $"api/{ApiV1}/genres";

    public static string BooksEndpoint => $"api/{ApiV1}/books";
    public static string ReadersEndpoint => $"api/{ApiV1}/readers";
    public static string LoansEndpoint => $"api/{ApiV1}/loans";

    public static string PenaltiesEndpoint => $"api/{ApiV1}/penalties";
    public static string TransactionsEndpoint => $"api/{ApiV1}/transactions";
}
