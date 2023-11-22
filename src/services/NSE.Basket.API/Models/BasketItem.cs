using FluentValidation;
using System.Text.Json.Serialization;

namespace NSE.Basket.API.Models;

public class BasketItem
{
    public BasketItem()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Value { get; set; }
    public string Image { get; set; }

    public Guid BasketId { get; set; }

    [JsonIgnore]
    public BasketCustomer BasketCustomer { get; set; }

    internal void AssociateBasket(Guid basketId)
    {
        BasketId = basketId;
    }

    internal decimal CalculateValue()
    {
        return Quantity * Value;
    }

    internal void AddUnits(int units)
    {
        Quantity += units;
    }

    internal void UpdateUnits(int units)
    {
        Quantity = units;
    }

    internal bool IsValid()
    {
        return new BasketItemValidation().Validate(this).IsValid;
    }

    public class BasketItemValidation : AbstractValidator<BasketItem>
    {
        public BasketItemValidation()
        {
            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid product id");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("The product name was not provided");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage(item => $"The minimum quantity for the {item.Name} is 1");

            RuleFor(c => c.Quantity)
                .LessThanOrEqualTo(BasketCustomer.MAX_ITEM_QUANTITY)
                .WithMessage(item => $"The maximum quantity of the {item.Name} is {BasketCustomer.MAX_ITEM_QUANTITY}");

            RuleFor(c => c.Value)
                .GreaterThan(0)
                .WithMessage(item => $"The value of the {item.Name} needs to be more than 0");
        }
    }
}
