using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Extensions
{
    public class BasketViewComponent : ViewComponent
    {
        private readonly IBasketService _basketService;
        public BasketViewComponent(IBasketService basketService)
        {
            _basketService = basketService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _basketService.GetBasket() ?? new BasketViewModel());
        }
    }
}
