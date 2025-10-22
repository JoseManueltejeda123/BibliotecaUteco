using BibliotecaUteco.Client.Utilities;

namespace BibliotecaUteco.Client.Responses;

public class UserResponse : BaseResponse
{
    public string FullName { get; set; } = "";

    public string Username { get; set; } = "";

    public string IdentityCardNumber { get; set; } = "";


    public string ProfilePictureUrl { get; set; } = "";

    public int SexId { get; set; }

    public string SexName => SexId == 1 ? "boy" : "girl";

    public ApplicationRoles Role {get; set;}
       
    public int RoleId {get; set;} = 0;
}
