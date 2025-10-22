using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Responses;

public class UserResponse : BaseResponse
{
    public string FullName { get; set; } = "";

    public string Username { get; set; } = "";

    public string IdentityCardNumber { get; set; } = "";


    public string ProfilePictureUrl { get; set; } = "";

    public int SexId { get; set; }

    public ApplicationSexes Sex {get; set;} 
    public ApplicationRoles Role {get; set;}
       
    public int RoleId {get; set;} = 0;
}
