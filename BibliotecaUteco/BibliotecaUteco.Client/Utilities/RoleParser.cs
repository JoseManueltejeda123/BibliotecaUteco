namespace BibliotecaUteco.Client.Utilities;

public static class RoleParser
{
    public static ApplicationRoles ParseRole(int roleId)
    {
        return roleId switch
        {
             1 => ApplicationRoles.Admin,
            _ => ApplicationRoles.Librarian
        };
    }
    
    
}

public enum ApplicationRoles
{
    Admin = 1,
    Librarian = 2,
}