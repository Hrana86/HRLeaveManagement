﻿using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HRLeaveManagement.MVC.CustomMiddleware;

public class RequestMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILocalStorageService _localStorageService;

    public RequestMiddleware(RequestDelegate next, ILocalStorageService localStorageService)
    {
        _next = next;
        _localStorageService = localStorageService;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            var ep = httpContext.Features.Get<IEndpointFeature>()?.Endpoint;
            var authAttr = ep?.Metadata?.GetMetadata<AuthorizeAttribute>();
            if (authAttr != null)
            {
                var tokenExists = _localStorageService.Exists("token");
                var tokenIsValid = true;
                if (tokenExists)
                {
                    var token = _localStorageService.GetStorageValue<string>("token");
                    JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                    var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(token);
                    var expiry = tokenContent.ValidTo;
                    if (expiry < DateTime.Now)
                    {
                        tokenIsValid = false;
                    }
                }

                if (!tokenIsValid || !tokenExists)
                {
                    await SignOutAndRedirect(httpContext);
                    return;
                }

                if (authAttr.Roles != null)
                {
                    var userRole = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                    if (authAttr.Roles.Contains(userRole) == false)
                    {
                        var path = $"/home/notauthorized";
                        httpContext.Response.Redirect(path);
                        return;
                    }
                }
            }
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        switch (exception)
        {
            case ApiException apiException:
                await SignOutAndRedirect(context);
                break;
            default:
                var path = $"/Home/Error";
                context.Response.Redirect(path);
                break;
        }
    }

    private static async Task SignOutAndRedirect(HttpContext httpContext)
    {
        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var path = $"/users/login";
        httpContext.Response.Redirect(path);
    }
}