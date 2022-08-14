using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IAuthenticationService = HRLeaveManagement.MVC.Contracts.IAuthenticationService;

namespace HRLeaveManagement.MVC.Services;

public class AuthenticationService : BaseHttpService, IAuthenticationService
{
    private readonly IHttpContextAccessor _contextAccessor;
    private JwtSecurityTokenHandler _tokenHandler;
    private readonly IMapper _mapper;

    public AuthenticationService(IClient client, ILocalStorageService localStorage, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        : base(localStorage, client)
    {
        _contextAccessor = httpContextAccessor;
        _tokenHandler = new JwtSecurityTokenHandler();
        _mapper = mapper;
    }
    public async Task<bool> Authenticate(string email, string password)
    {
        try
        {
            AuthRequest authRequest = new() { Email = email, Password = password };
            var authResponse = await _client.LoginAsync(authRequest);

            if (authResponse.Token != string.Empty)
            {
                var tokenContent = _tokenHandler.ReadJwtToken(authResponse.Token);
                var claims = ParseClaims(tokenContent);
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
                var login = _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);
                _localStorage.SetStorageValue("token", authResponse.Token);

                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task Logout()
    {
        _localStorage.ClearStorage(new List<string> { "token" });
        await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public async Task<bool> Register(RegisterVM registration)
    {
        RegistrationRequest registrationRequest = _mapper.Map<RegistrationRequest>(registration);

        var response = await _client.RegisterAsync(registrationRequest);

        if (!string.IsNullOrEmpty(response.UserId))
        {
            await Authenticate(registration.Email, registration.Password);
            return true;
        }

        return false;
    }

    private IList<Claim> ParseClaims(JwtSecurityToken tokenContent)
    {
        var claims = tokenContent.Claims.ToList();
        claims.Add(new Claim(ClaimTypes.Name, tokenContent.Subject));
        return claims;
    }
}