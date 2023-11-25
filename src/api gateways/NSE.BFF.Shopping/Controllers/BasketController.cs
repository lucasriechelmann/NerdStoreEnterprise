using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.BFF.Shopping.Models;
using NSE.BFF.Shopping.Services;
using NSE.WebAPI.Core.Controllers;

namespace NSE.BFF.Shopping.Controllers
{
    [Authorize]
    public class BasketController : MainController
    {
        private readonly IOrderService _orderService;
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public BasketController(IOrderService orderService, ICatalogService catalogService, IBasketService basketService)
        {
            _orderService = orderService;
            _catalogService = catalogService;
            _basketService = basketService;
        }

        [HttpGet]
        [Route("shopping/basket")]
        public async Task<IActionResult> Index() => CustomResponse(await _basketService.GetBasket());
        [HttpGet]
        [Route("shopping/basket-quantity")]
        public async Task<int> GetBasketQuantity()
        {
            var basket = await _basketService.GetBasket();
            return basket?.Items.Sum(x => x.Quantity) ?? 0;
        }

        [HttpPost]
        [Route("shopping/basket/items")]
        public async Task<IActionResult> AddBasketItem(BasketItemDTO item)
        {
            var product = await _catalogService.GetById(item.ProductId);
            
            await ValidateBasketItem(product, item.Quantity, true);

            if (!IsOperationValid()) 
                return CustomResponse();

            item.Name = product.Name;
            item.Value = product.Value;
            item.Image = product.Image;

            var response = await _basketService.AddBasketItem(item);

            return CustomResponse(response);
        }

        [HttpPut]
        [Route("shopping/basket/items/{productId}")]
        public async Task<IActionResult> UpdateBasketItem(Guid productId, BasketItemDTO item)
        {
            var product = await _catalogService.GetById(productId);

            await ValidateBasketItem(product, item.Quantity);
            if (!IsOperationValid()) 
                return CustomResponse();

            var response = await _basketService.UpdateBasketItem(productId, item);

            return CustomResponse(response);
        }

        [HttpDelete]
        [Route("shopping/basket/items/{productId}")]
        public async Task<IActionResult> RemoveBasketItem(Guid productId)
        {
            var product = await _catalogService.GetById(productId);

            if (product == null)
            {
                AddError("Produto inexistente!");
                return CustomResponse();
            }

            var response = await _basketService.RemoveBasketItem(productId);

            return CustomResponse(response);
        }
        [HttpPost]
        [Route("shopping/basket/apply-voucher")]
        public async Task<IActionResult> ApplyVoucher([FromBody] string voucherCode)
        {
            var voucher = await _orderService.GetVoucherByCode(voucherCode);
            if (voucher is null)
            {
                AddError("Voucher inválido ou não encontrado!");
                return CustomResponse();
            }

            var response = await _basketService.ApplyBasketVoucher(voucher);

            return CustomResponse(response);
        }
        private async Task ValidateBasketItem(ProductDTO product, int quantity, bool addProduct = false)
        {
            if (product == null) AddError("Produto inexistente!");
            if (quantity < 1) AddError($"Escolha ao menos uma unidade do produto {product.Name}");

            var basket = await _basketService.GetBasket();
            var basketItem = basket.Items.FirstOrDefault(p => p.ProductId == product.Id);

            if (basketItem != null && addProduct && basketItem.Quantity + quantity > product.StockQuantity)
            {
                AddError($"O produto {product.Name} possui {product.StockQuantity} unidades em estoque, você selecionou {quantity}");
                return;
            }

            if (quantity > product.StockQuantity) AddError($"O produto {product.Name} possui {product.StockQuantity} unidades em estoque, você selecionou {quantity}");
        }
    }
}
