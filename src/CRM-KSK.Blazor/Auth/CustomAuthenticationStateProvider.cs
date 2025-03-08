using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CRM_KSK.Blazor.Auth;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public CustomAuthenticationStateProvider(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Получаем токен из localStorage
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "jwt");

        if (string.IsNullOrEmpty(token))
        {
            // Если токена нет, возвращаем анонимного пользователя
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        // Декодируем токен и извлекаем claims
        var claims = ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    public async Task NotifyUserAuthenticationAsync(string token)
    {
        // Сохраняем токен в localStorage
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "jwt", token);

        // Уведомляем Blazor об изменении состояния аутентификации
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task NotifyUserLogoutAsync()
    {
        // Удаляем токен из localStorage
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "jwt");

        // Уведомляем Blazor об изменении состояния аутентификации
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        return token.Claims;
    }
}
