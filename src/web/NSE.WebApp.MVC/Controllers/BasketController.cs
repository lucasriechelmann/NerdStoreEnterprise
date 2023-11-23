using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers;
[Authorize]
public class BasketController : MainController
{
    IBasketService _basketService;
    ICatalogService _catalogService;

    public BasketController(IBasketService basketService, ICatalogService catalogService)
    {
        _basketService = basketService;
        _catalogService = catalogService;
    }
    [Route("basket")]
    public async Task<IActionResult> Index() => View(await _basketService.GetBasket());
    [Route("basket/add-item")]
    public async Task<IActionResult> AddBasketItem(BasketItemViewModel item)
    {
        var product = await _catalogService.GetById(item.ProductId);
        ValidateBasketItem(product, item.Quantity);
        
        if(!IsOperationValid()) 
            return View("Index", await _basketService.GetBasket());

        item.Name = product.Name;
        item.Value = product.Value;
        item.Image = product.Image;

        var response = await _basketService.AddBasketItem(item);

        if (ResponseHasErrors(response)) 
            return View("Index", await _basketService.GetBasket());

        return RedirectToAction("Index");
    }
    [HttpPost]
    [Route("basket/update-item")]
    public async Task<IActionResult> UpdateBasketItem(Guid productId, int quantity)
    {
        var product = await _catalogService.GetById(productId);
        ValidateBasketItem(product, quantity);

        if (!IsOperationValid())
            return View("Index", await _basketService.GetBasket());

        var item = new BasketItemViewModel { ProductId = productId, Quantity = quantity };
        var response = await _basketService.UpdateBasketItem(productId, item);

        if (ResponseHasErrors(response))
            return View("Index", await _basketService.GetBasket());

        return RedirectToAction("Index");
    }
    [HttpPost]
    [Route("basket/remove-item")]
    public async Task<IActionResult> RemoveBasketItem(Guid productId)
    {
        var product = await _catalogService.GetById(productId);
        if (product == null)
        {
            AddError("Non-existent product!");
            return RedirectToAction("Index", await _basketService.GetBasket());
        }            

        var response = await _basketService.RemoveBasketItem(productId);

        if (ResponseHasErrors(response))
            return View("Index", await _basketService.GetBasket());

        return RedirectToAction("Index");
    }
    private void ValidateBasketItem(ProductViewModel product, int quantity)
    {
        if (product == null) AddError("Non-existent product!");
        if (quantity < 1) AddError($"Choose at least one unit of the product produto {product.Name}");
        if (quantity > product.StockQuantity) AddError($"The product {product.Name} has {product.StockQuantity} units in stock, você selected {quantity}");
    }
}
