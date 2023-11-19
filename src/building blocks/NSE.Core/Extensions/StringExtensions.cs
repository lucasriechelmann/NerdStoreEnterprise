namespace NSE.Core.Util;
public static class StringExtensions
{
    public static string OnlyNumbers(this string str) => new string(str.Where(char.IsDigit).ToArray());
}
