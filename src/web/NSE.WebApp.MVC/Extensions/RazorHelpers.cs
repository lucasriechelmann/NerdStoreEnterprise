using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Cryptography;
using System.Text;

namespace NSE.WebApp.MVC.Extensions;
public static class RazorHelpers
{
    public static string HashEmailForGravatar(this RazorPage page, string email)
    {
        var md5Hasher = MD5.Create();
        var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
        var sBuilder = new StringBuilder();
        foreach (var t in data)
        {
            sBuilder.Append(t.ToString("x2"));
        }
        return sBuilder.ToString();
    }

    public static string CurrencyFormat(this RazorPage page, decimal value) =>
        CurrencyFormat(value);
    private static string CurrencyFormat(decimal value) =>
        value > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", value) : "Free";
    public static string StockMessage(this RazorPage page, int quantity) =>
        quantity > 0 ? $"Only {quantity} in stock!" : "Product out of stock!";
    public static string UnitsPerProduct(this RazorPage page, int units) =>
        units > 1 ? $"{units} units" : $"{units} unit";
    public static string SelectOptionsByQuantity(this RazorPage page, int quantity, int selectedValue = 0)
    {
        var sb = new StringBuilder();
        for (var i = 1; i <= quantity; i++)
        {
            var selected = "";
            if (i == selectedValue) selected = "selected";
            sb.Append($"<option {selected} value='{i}'>{i}</option>");
        }

        return sb.ToString();
    }
    public static string UnitsByTotalValueProduct(this RazorPage page, int unit, decimal value) =>
        $"{unit}x {CurrencyFormat(value)} = Total: {CurrencyFormat(value * unit)}";
    public static string ShowStatus(this RazorPage page, int status)
    {
        var statusMessage = "";
        var statusClass = "";

        switch (status)
        {
            case 1:
                statusMessage = "Awaiting payment";
                statusClass = "badge badge-secondary";
                break;
            case 2:
                statusMessage = "Paid";
                statusClass = "badge badge-success";
                break;
            case 3:
                statusMessage = "Declined";
                statusClass = "badge badge-danger";
                break;
            case 4:
                statusMessage = "Cancelled";
                statusClass = "badge badge-danger";
                break;
            case 5:
                statusMessage = "Delivered";
                statusClass = "badge badge-success";
                break;
            case 6:
                statusMessage = "Awaiting delivery";
                statusClass = "badge badge-warning";
                break;
            case 7:
                statusMessage = "Delivering";
                statusClass = "badge badge-info";
                break;
            default:
                break;
        }

        return $"<span class='badge badge-{statusClass}'>{statusMessage}</span>";
    }
}
