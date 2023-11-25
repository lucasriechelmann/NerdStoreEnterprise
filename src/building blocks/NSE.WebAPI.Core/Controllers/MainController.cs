using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSE.Core.Communication;

namespace NSE.WebAPI.Core.Controllers;
[ApiController]
public abstract class MainController : Controller
{
    protected ICollection<string> Errors = new List<string>();
    protected IActionResult CustomResponse(object result = null)
    {
        if (IsOperationValid())
            return Ok(result);

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
        {
            {"Messages", Errors.ToArray()}
        }));
    }
    protected IActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(e => e.Errors);

        foreach (var error in errors)
            AddError(error.ErrorMessage);

        return CustomResponse();
    }
    protected IActionResult CustomResponse(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
            AddError(error.ErrorMessage);

        return CustomResponse();
    }
    protected IActionResult CustomResponse(ResponseResult response)
    {
        IsThereAnyErrors(response);

        return CustomResponse();
    }
    protected bool IsThereAnyErrors(ResponseResult response)
    {
        if (response == null || response.Errors is null || !response.Errors.Messages.Any()) return false;

        foreach (var mensagem in response.Errors.Messages)
        {
            AddError(mensagem);
        }

        return true;
    }
    protected bool IsOperationValid() => !Errors.Any();
    protected void AddError(string error) => Errors.Add(error);
    protected void ClearErrors() => Errors.Clear();
}
