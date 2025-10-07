namespace BibliotecaUteco.Client.Responses;

public class UserResponse : BaseResponse
{
    public string FullName { get; set; } = "";
    
    public string UserName { get; set; } = "";

    public string IdentityCardNumber { get; set; } = "";

    public string RoleName { get; set; } = "";

    public string ProfilePictureUrl { get; set; } = "";

    public int RoleId { get; set; } = 0;
    
}