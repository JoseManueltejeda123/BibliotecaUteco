namespace BibliotecaUteco.Client.Responses;

public class UserResponse : BaseResponse
{
    public string FullName { get; set; } = "";
    
    public string Username { get; set; } = "";

    public string IdentityCardNumber { get; set; } = "";

    public string RoleName { get; set; } = "";

    public string ProfilePictureUrl { get; set; } = "";

    public string _roleName => RoleId switch
    {
        1 => "Admin",
        _ => "Bibliotecario"
    };

    public int RoleId { get; set; } = 0;
    
}