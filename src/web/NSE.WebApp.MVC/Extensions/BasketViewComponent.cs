using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Extensions
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly IShoppingBffService _shoppingBffService;
        public BasketViewComponent(IShoppingBffService shoppingBffService)
        {
            _shoppingBffService = shoppingBffService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _shoppingBffService.GetBasketQuantity());
        }
    }
}
