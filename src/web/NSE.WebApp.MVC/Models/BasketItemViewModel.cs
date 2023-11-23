namespace NSE.WebApp.MVC.Models;

public class BasketItemViewModel
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Value { get; set; }
    public string Image { get; set; }
}
