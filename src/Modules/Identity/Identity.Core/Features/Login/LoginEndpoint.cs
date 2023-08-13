using System.Net;
using Common.Api;
using FastEndpoints;
using Identity.Core.Common.Identity.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Features.Login;

public sealed class LoginEndpoint : EndpointBase<LoginRequest, Results<Ok<LoginResponse>, ProblemDetails>>
{
    private const string InvalidCredentialsErrorDescription = "Неверные логин и/или пароль";
    
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;

    /// <inheritdoc />
    public LoginEndpoint(
        SignInManager<User> signInManager, 
        UserManager<User> userManager, 
        TokenService tokenService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    /// <inheritdoc />
    public override void Configure()
    {
        Post("login");
        Group<UserGroup>();

        AllowAnonymous();

        ConfigureSwaggerDescription(
            new LoginSummary(), 
            HttpStatusCode.OK,
            HttpStatusCode.BadRequest,
            HttpStatusCode.InternalServerError);
    }

    /// <inheritdoc />
    public override async Task<Results<Ok<LoginResponse>, ProblemDetails>> ExecuteAsync(
        LoginRequest req, 
        CancellationToken ct)
    {
        var user = await _userManager.FindByNameAsync(req.Username);

        if (user is null)
        {
            AddError(InvalidCredentialsErrorDescription);
            return new ProblemDetails(ValidationFailures);
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, req.Password, false);

        if (!signInResult.Succeeded)
        {
            AddError(InvalidCredentialsErrorDescription);
            return new ProblemDetails(ValidationFailures);
        }

        var token = _tokenService.GenerateToken(user);

        return TypedResults.Ok(new LoginResponse(user.Id, token));
    }
}