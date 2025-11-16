namespace BibliotecaUteco.Settings;

public abstract class CorsPolicies
{
    public static string DefaultPolicy => "default";
}

public abstract class CorsAllowedDomains
{

    #if DEBUG
        public static string DefaultDomain => "http://localhost:5000";
    #else
        public static string DefaultDomain => "http://bibliouteco-001-site1.anytempurl.com/books";
    #endif
}
