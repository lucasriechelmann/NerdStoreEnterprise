using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace NSE.Basket.API.Models;

public class BasketCustomer
{
    internal const int MAX_ITEM_QUANTITY = 5;

    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalValue { get; set; }
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    public ValidationResult ValidationResult { get; set; }

    public BasketCustomer(Guid clienteId)
    {
        Id = Guid.NewGuid();
        CustomerId = clienteId;
    }

    public BasketCustomer() { }

    internal void CalculateBasketValue() => TotalValue = Items.Sum(p => p.CalculateValue());
    internal bool BasketExistingItem(BasketItem item) => Items.Any(p => p.ProductId == item.ProductId);
    internal BasketItem GetProductById(Guid productId) => Items.FirstOrDefault(p => p.ProductId == productId);

    internal void AddItem(BasketItem item)
    {
        item.AssociateBasket(Id);

        if (BasketExistingItem(item))
        {
            var existingItem = GetProductById(item.ProductId);
            existingItem.AddUnits(item.Quantity);

            item = existingItem;
            Items.Remove(existingItem);
        }

        Items.Add(item);
        CalculateBasketValue();
    }

    internal void UpdateItem(BasketItem item)
    {
        item.AssociateBasket(Id);

        var existingItem = GetProductById(item.ProductId);

        Items.Remove(existingItem);
        Items.Add(item);

        CalculateBasketValue();
    }

    internal void UpdateUnits(BasketItem item, int units)
    {
        item.UpdateUnits(units);
        UpdateItem(item);
    }

    internal void RemoveItem(BasketItem item)
    {
        Items.Remove(GetProductById(item.ProductId));
        CalculateBasketValue();
    }

    internal bool IsValid()
    {
        var erros = Items.SelectMany(i => new BasketItem.BasketItemValidation().Validate(i).Errors).ToList();
        erros.AddRange(new BasketCustomerValidation().Validate(this).Errors);
        ValidationResult = new ValidationResult(erros);

        return ValidationResult.IsValid;
    }

    public class BasketCustomerValidation : AbstractValidator<BasketCustomer>
    {
        public BasketCustomerValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customer not recognized");

            RuleFor(c => c.Items.Count)
                .GreaterThan(0)
                .WithMessage("The basket does not have items");

            RuleFor(c => c.TotalValue)
                .GreaterThan(0)
                .WithMessage("The total value of the basket needs to be greater than 0");
        }
    }
}
