using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponseHasErrors(ResponseResult response)
        {
            if(response == null || response.Errors is null || !response.Errors.Messages.Any()) 
                return false;

            foreach(var message in response.Errors.Messages)
            {
                ModelState.AddModelError(string.Empty, message);
            }

            return true;
        }
        protected void AddError(string message) => ModelState.AddModelError(string.Empty, message);
        protected bool IsOperationValid() => ModelState.ErrorCount == 0;
    }
}
