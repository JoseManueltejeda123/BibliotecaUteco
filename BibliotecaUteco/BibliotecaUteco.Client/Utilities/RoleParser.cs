namespace BibliotecaUteco.Client.Utilities;

public static class RoleParser
{
    public static int ParseRole(string roleName)
    {
        return roleName switch
        {
             "Administrador" => 1,
            _ => 2
        };
    }
}