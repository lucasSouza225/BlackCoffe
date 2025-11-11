using BlackCoffe.UI.Services.Implementations;

namespace BlackCoffe.UI.Middleware;

public class SessionAuthMiddleware
{
    private readonly RequestDelegate _next;

    public SessionAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, UserContextService userContextService)
    {
        // Configura o User.Identity baseado na sessão
        context.User = userContextService.CreateClaimsPrincipal();
        await _next(context);
    }
}