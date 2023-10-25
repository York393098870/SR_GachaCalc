using System.Text.RegularExpressions;

namespace GUI_Avalonia.Tools;

public static partial class InputValidationCheck
{
    public static bool OnlyAllowPositiveNumbers(string valueNeedToCheck)
        //判断是否为正整数
    {
        var regex = MyRegex();
        if (!regex.IsMatch(valueNeedToCheck))
        {
            //不满足正则表达式


            return false;
        }

        return int.TryParse(valueNeedToCheck, out var intValueNeedToCheck) && intValueNeedToCheck >= 1;
    }

    [GeneratedRegex("^[0-9]+$")]
    private static partial Regex MyRegex();
}