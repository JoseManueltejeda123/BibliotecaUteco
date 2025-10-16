using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BibliotecaUteco.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BibliotecaUteco.Dependencies;

public static class JwtDependencies
{
    public static IServiceCollection AddJwtServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<JwtSettings>()
            .BindConfiguration("JWT")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Add("role", ClaimTypes.Role);
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Add("sub", ClaimTypes.NameIdentifier);
        
        services.AddAuthorization(opts =>
        {
            opts.AddPolicy(
                AuthorizationPolicies.AllowAuthorizedUsers,
                policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(
                        new string[]
                        {
                            nameof(RolesHelper.Librarian),
                            nameof(RolesHelper.Admin)
                           
                        }
                    );
                }
            );
            opts.AddPolicy(
                AuthorizationPolicies.AllowAdminsOnly,
                policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole(
                        new string[]
                        {
                            nameof(RolesHelper.Admin),
                        }
                    );
                }
            );
           
        });
        
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,

                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            configuration["JWT:Key"] ?? throw new NullReferenceException("JWT KEY")
                        )
                    ),
                };
                options.Events = new JwtBearerEvents()
                {
                    OnForbidden = context =>
                    {
                        context.Response.OnStarting(async () =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsJsonAsync(
                             ApiResult<object>.BuildFailure(HttpStatus.Forbidden,"No tienes permisos para acceder a este recurso")


                            );
                        });

                        return Task.CompletedTask;
                    },
                    OnChallenge = c =>
                    {
                        c.HandleResponse();

                        var hasToken =
                            c.Request.Headers.TryGetValue("Authorization", out var authHeader)
                            && !string.IsNullOrWhiteSpace(authHeader)
                            && authHeader
                                .ToString()
                                .StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase);

                        if (!hasToken)
                        {
                            c.Response.OnStarting(async () =>
                            {
                                c.Response.StatusCode = 401;
                                c.Response.ContentType = "application/json";
                                
                                await c.Response.WriteAsJsonAsync(
                                    ApiResult<object>.BuildFailure(HttpStatus.Unauthorized,"El token de autorización no ha podido ser detectado")

                                
                                );
                            });
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.OnStarting(async () =>
                        {
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsJsonAsync(
                                ApiResult<object>.BuildFailure(HttpStatus.Unauthorized,"Su identidad no ha podido ser comprobada.")

                            

                            );
                        });

                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    },
                };
            });

        services.AddScoped<JwtBuilder>();

        return services;
    }
}