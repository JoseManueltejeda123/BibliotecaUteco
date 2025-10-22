namespace BibliotecaUteco.Client.Utilities;

public static class RoleParser
{
    public static ApplicationRoles ParseRole(int roleId)
    {
        return roleId switch
        {
             1 => ApplicationRoles.Admin,
            _ => ApplicationRoles.Bibliotecario
        };
    }
    
    
}

public enum ApplicationRoles
{
    Admin = 1,
    Bibliotecario = 2,
}