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
    
    public static string ParseSexFromId(int sexId)
    {
        return sexId switch
        {
            1 => "Masculino",
            _ => "Femenino"
        };
    }
}