using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using System.Diagnostics;

namespace NSE.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var errorModel = new ErrorViewModel(id);

            switch (id)
            {
                case 500:
                    errorModel.Message = "An error has occurred! Please try again later or contact our support.";
                    errorModel.Title = "An error has occurred!";
                    break;
                case 404:
                    errorModel.Message = "The page you are looking for does not exist! <br />Please contact our support.";
                    errorModel.Title = "Ops! Page not found.";
                    break;
                case 403:
                    errorModel.Message = "You do not have permission to do that.";
                    errorModel.Title = "Access denied";
                    break;
                default:
                    return StatusCode(404);
            }

            return View("Error", errorModel);
        }
    }
}