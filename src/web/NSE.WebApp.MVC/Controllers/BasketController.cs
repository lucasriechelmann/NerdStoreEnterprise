using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers;
[Authorize]
public class BasketController : MainController
{
    readonly IShoppingBffService _shoppingBffService;

    public BasketController(IShoppingBffService shoppingBffService)
    {
        _shoppingBffService = shoppingBffService;
    }

    [Route("basket")]
    public async Task<IActionResult> Index() => View(await _shoppingBffService.GetBasket());
    [Route("basket/add-item")]
    public async Task<IActionResult> AddBasketItem(BasketItemViewModel item)
    {
        var response = await _shoppingBffService.AddBasketItem(item);
       
        if (ResponseHasErrors(response)) 
            return View("Index", await _shoppingBffService.GetBasket());

        return RedirectToAction("Index");
    }
    [HttpPost]
    [Route("basket/update-item")]
    public async Task<IActionResult> UpdateBasketItem(Guid productId, int quantity)
    {
        var item = new BasketItemViewModel { ProductId = productId, Quantity = quantity };
        var response = await _shoppingBffService.UpdateBasketItem(productId, item);

        if (ResponseHasErrors(response))
            return View("Index", await _shoppingBffService.GetBasket());

        return RedirectToAction("Index");
    }
    [HttpPost]
    [Route("basket/remove-item")]
    public async Task<IActionResult> RemoveBasketItem(Guid productId)
    {
        var response = await _shoppingBffService.RemoveBasketItem(productId);

        if (ResponseHasErrors(response))
            return View("Index", await _shoppingBffService.GetBasket());

        return RedirectToAction("Index");
    }
    [HttpPost]
    [Route("basket/apply-voucher")]
    public async Task<IActionResult> ApplyVoucher(string voucherCode)
    {
        var response = await _shoppingBffService.ApplyBasketVoucher(voucherCode);

        if (ResponseHasErrors(response))
            return View("Index", await _shoppingBffService.GetBasket());

        return RedirectToAction("Index");
    }
}
