namespace NSE.WebApp.MVC.Models;

public class BasketViewModel
{
    public decimal TotalValue { get; set; }
    public List<BasketItemViewModel> Items { get; set; } = new List<BasketItemViewModel>();
}
