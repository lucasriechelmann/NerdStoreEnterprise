using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.WebAPI.Core.Controllers;

namespace NSE.BFF.Shopping.Controllers
{
    [Authorize]
    public class BasketController : MainController
    {
        [HttpGet]
        [Route("shopping/basket")]
        public async Task<IActionResult> Index()
        {
            return CustomResponse();
        }

        [HttpGet]
        [Route("shopping/basket-quantity")]
        public async Task<IActionResult> GetBasketQuantity()
        {
            return CustomResponse();
        }

        [HttpPost]
        [Route("shopping/basket/items")]
        public async Task<IActionResult> AddBasketItem()
        {
            return CustomResponse();
        }

        [HttpPut]
        [Route("shopping/basket/items/{productId}")]
        public async Task<IActionResult> UpdateBasketItem()
        {
            return CustomResponse();
        }

        [HttpDelete]
        [Route("shopping/basket/items/{productId}")]
        public async Task<IActionResult> RemoveBasketItem()
        {
            return CustomResponse();
        }
    }
}
