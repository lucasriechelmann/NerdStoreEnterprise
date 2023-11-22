using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSE.Basket.API.Data;
using NSE.Basket.API.Models;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.User;

namespace NSE.Basket.API.Controllers;
public class BasketController : MainController
{
    private readonly IAspNetUser _user;
    private readonly BasketContext _context;

    public BasketController(IAspNetUser user, BasketContext context)
    {
        _user = user;
        _context = context;
    }

    [HttpGet("basket")]
    public async Task<BasketCustomer> GetBasket()
    {
        return await GetBasketCustomer() ?? new BasketCustomer();
    }

    [HttpPost("basket")]
    public async Task<IActionResult> AddItemBasket(BasketItem item)
    {
        var basket = await GetBasketCustomer();

        if (basket == null)
            ManipulateNewBasket(item);
        else
            ManipulateExistingBasket(basket, item);

        if (!IsOperationValid()) return CustomResponse();

        await PersistData();
        return CustomResponse();
    }

    [HttpPut("basket/{produtoId}")]
    public async Task<IActionResult> UpdateBasketItem(Guid productId, BasketItem item)
    {
        var basket = await GetBasketCustomer();
        var basketItem = await GetBasketItemValidated(productId, basket, item);
        if (basketItem == null) return CustomResponse();

        basket.UpdateUnits(basketItem, item.Quantity);

        ValidateBasket(basket);
        if (!IsOperationValid()) return CustomResponse();

        _context.BasketItems.Update(basketItem);
        _context.BasketCustomers.Update(basket);

        await PersistData();
        return CustomResponse();
    }

    [HttpDelete("basket/{produtoId}")]
    public async Task<IActionResult> RemoveBasketItem(Guid productId)
    {
        var basket = await GetBasketCustomer();

        var basketItem = await GetBasketItemValidated(productId, basket);
        if (basketItem == null) return CustomResponse();

        ValidateBasket(basket);
        if (!IsOperationValid()) return CustomResponse();

        basket.RemoveItem(basketItem);

        _context.BasketItems.Remove(basketItem);
        _context.BasketCustomers.Update(basket);

        await PersistData();
        return CustomResponse();
    }

    private async Task<BasketCustomer> GetBasketCustomer()
    {
        return await _context.BasketCustomers
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CustomerId == _user.GetUserId());
    }
    private void ManipulateNewBasket(BasketItem item)
    {
        var basket = new BasketCustomer(_user.GetUserId());
        basket.AddItem(item);

        ValidateBasket(basket);
        _context.BasketCustomers.Add(basket);
    }
    private void ManipulateExistingBasket(BasketCustomer basket, BasketItem item)
    {
        var existingProductItem = basket.BasketExistingItem(item);

        basket.AddItem(item);
        ValidateBasket(basket);

        if (existingProductItem)
        {
            _context.BasketItems.Update(basket.GetProductById(item.ProductId));
        }
        else
        {
            _context.BasketItems.Add(item);
        }

        _context.BasketCustomers.Update(basket);
    }
    private async Task<BasketItem> GetBasketItemValidated(Guid productId, BasketCustomer basket, BasketItem item = null)
    {
        if (item != null && productId != item.ProductId)
        {
            AddError("The item does not correspond to what was stated");
            return null;
        }

        if (basket == null)
        {
            AddError("Basket not found");
            return null;
        }

        var basketItem = await _context.BasketItems
            .FirstOrDefaultAsync(i => i.BasketId == basket.Id && i.ProductId == productId);

        if (basketItem == null || !basket.BasketExistingItem(basketItem))
        {
            AddError("The item is not in the basket");
            return null;
        }

        return basketItem;
    }
    private async Task PersistData()
    {
        var result = await _context.SaveChangesAsync();
        if (result <= 0) AddError("Unable to persist data in the database");
    }
    private bool ValidateBasket(BasketCustomer basket)
    {
        if (basket.IsValid()) return true;

        basket.ValidationResult.Errors.ToList().ForEach(e => AddError(e.ErrorMessage));
        return false;
    }
}
