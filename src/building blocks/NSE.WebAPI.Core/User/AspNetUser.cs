using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace NSE.WebAPI.Core.User;

public class AspNetUser : IAspNetUser
{
    private readonly IHttpContextAccessor _accessor;
    public AspNetUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    public string Name => _httpContext.User.Identity.Name;
    public IEnumerable<Claim> GetClaims() => _httpContext.User.Claims;
    public HttpContext GetHttpContext() => _httpContext;
    public string GetUserEmail() => IsAuthenticated() ? _httpContext.User.GetUserEmail() : "";
    public Guid GetUserId() => IsAuthenticated() ? Guid.Parse(_httpContext.User.GetUserId()) : Guid.Empty;
    public string GetUserToken() => IsAuthenticated() ? _httpContext.User.GetUserToken() : "";
    public bool HasRole(string role) => _httpContext.User.IsInRole(role);
    public bool IsAuthenticated() => _httpContext.User.Identity.IsAuthenticated;
    HttpContext _httpContext => _accessor.HttpContext;
}
