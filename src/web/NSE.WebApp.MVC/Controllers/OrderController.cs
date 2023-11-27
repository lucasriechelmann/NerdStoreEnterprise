using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers
{
    public class OrderController : MainController
    {
        private readonly ICustomerService _customerService;
        private readonly IShoppingBffService _shoppingBffService;
        public OrderController(ICustomerService customerService, IShoppingBffService shoppingBffService)
        {
            _customerService = customerService;
            _shoppingBffService = shoppingBffService;
        }
        [HttpGet]
        [Route("delivery-address")]
        public async Task<IActionResult> DeliveryAddress()
        {
            var basket = await _shoppingBffService.GetBasket();
            if (basket?.Items.Count == 0)
                return RedirectToAction("Index", "Basket");

            var address = await _customerService.GetAddress();
            var order = _shoppingBffService.MapToOrder(basket, address);

            return View(order);
        }
        [HttpGet]
        [Route("payment")]
        public async Task<IActionResult> Payment()
        {
            var basket = await _shoppingBffService.GetBasket();

            if (basket.Items.Count == 0) 
                return RedirectToAction("Index", "Basket");

            var order = _shoppingBffService.MapToOrder(basket, null);

            return View(order);
        }
        [HttpPost]
        [Route("finish-order")]
        public async Task<IActionResult> FinishOrder(OrderTransactionViewModel orderTransaction)
        {
            if (!ModelState.IsValid) 
                return View("Payment", _shoppingBffService
                    .MapToOrder(await _shoppingBffService.GetBasket(), null));

            var retorno = await _shoppingBffService.FinishOrder(orderTransaction);

            if (ResponseHasErrors(retorno))
            {
                var basket = await _shoppingBffService.GetBasket();
                
                if (basket.Items.Count == 0) 
                    return RedirectToAction("Index", "Basket");

                var orderMap = _shoppingBffService.MapToOrder(basket, null);
                return View("Payment", orderMap);
            }

            return RedirectToAction("OrderCompleted");
        }

        [HttpGet]
        [Route("order-completed")]
        public async Task<IActionResult> OrderCompleted() =>
            View("ConfirmedOrder", await _shoppingBffService.GetLastOrder());
        [HttpGet]
        [Route("my-orders")]
        public async Task<IActionResult> MyOrders() =>
            View(await _shoppingBffService.GetListByCustomerId());
    }
}
