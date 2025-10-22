namespace BibliotecaUteco.Client.Utilities;

public class SexParser
{
    
    
    public static ApplicationSexes ParseSex(int sexId)
    {
        return sexId switch
        {
            1 => ApplicationSexes.Masculino,
            _ => ApplicationSexes.Femenino
        };
    }
}

public enum ApplicationSexes
{
    Masculino = 1,
    Femenino = 2
}