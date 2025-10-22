namespace BibliotecaUteco.Client.Utilities;

public class SexParser
{
    public static int ParseSex(string sexName)
    {
        return sexName switch
        {
             "Masculino" => 1,
            _ => 2
        };
    }
    
    public static ApplicationSexes ParseSexFromId(int sexId)
    {
        return sexId switch
        {
            1 => ApplicationSexes.Male,
            _ => ApplicationSexes.Female
        };
    }
}

public enum ApplicationSexes
{
    Male = 1,
    Female = 2
}