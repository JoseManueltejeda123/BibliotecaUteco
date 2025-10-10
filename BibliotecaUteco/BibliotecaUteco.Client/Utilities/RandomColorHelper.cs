using LumexUI.Common;

namespace BibliotecaUteco.Client.Utilities;

public static class RandomColorHelper
{
    public static ThemeColor GetRandomColor(){

        var random = new Random();
        int number = random.Next(1, 7);

        return number switch{

            1 => ThemeColor.Default,
            2 => ThemeColor.Primary,
            3 => ThemeColor.Info,
            4 => ThemeColor.Danger,
            5 => ThemeColor.Secondary,
            6 => ThemeColor.Success,
            7 => ThemeColor.Warning,
            _ => ThemeColor.Primary
        };
    }
}