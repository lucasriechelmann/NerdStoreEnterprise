using NSE.Order.Domain.Orders;

namespace NSE.Order.API.Application.DTO;
public class OrderItemDTO
{
    public OrderItemDTO()
    {        
    }
    public OrderItemDTO(Guid orderId, Guid productId, string name, decimal value, string image, int quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        Name = name;
        Value = value;
        Image = image;
        Quantity = quantity;
    }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public decimal Value { get; set; }
    public string Image { get; set; }
    public int Quantity { get; set; }
    public static explicit operator OrderItemDTO(OrderItem item) =>
        new OrderItemDTO(item.OrderId, item.ProductId, item.ProductName, item.UnitValue, item.ProductImage, item.Quantity);
    public static explicit operator OrderItem(OrderItemDTO item) =>
        new OrderItem(item.ProductId, item.Name, item.Quantity, item.Value, item.Image);
}
