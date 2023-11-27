namespace NSE.WebApp.MVC.Models;
public class OrderViewModel
{
    public int Code { get; set; }
    public int Status { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalValue { get; set; }
    public decimal Discount { get; set; }
    public bool VoucherUsed { get; set; }   
    public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
    public AddressViewModel Address { get; set; }
}