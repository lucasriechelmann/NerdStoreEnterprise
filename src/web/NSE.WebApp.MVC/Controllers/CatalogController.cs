using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers
{
    public class CatalogController : MainController
    {
        private readonly ICatalogServiceRefit _catalogService;
        
        public CatalogController(ICatalogServiceRefit catalogService)
        {
            _catalogService = catalogService;
        }
        [HttpGet]
        [Route("")]
        [Route("products")]
        public async Task<IActionResult> Index()
        {
            var products = await _catalogService.GetAll();
            return View(products);
        }
        [HttpGet("product-detail/{id}")]
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            var product = await _catalogService.GetById(id);
            return View(product);
        }
    }
}
