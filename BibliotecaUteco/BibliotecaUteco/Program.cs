using BibliotecaUteco;
using BibliotecaUteco.Client.Pages;
using BibliotecaUteco.Components;
using BibliotecaUteco.Dependencies;
using BibliotecaUteco.Helpers;
using BibliotecaUteco.Settings;
using Microsoft.AspNetCore.Http.Timeouts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Logging.AddSimpleConsole(options =>
{
    options.TimestampFormat = "[yyyy-MM-dd HH:mm:ss] ";
    options.IncludeScopes = false;
    options.SingleLine = false;
});
builder.Services.AddServerServices(builder);
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: CorsPolicies.DefaultPolicy,
        policy =>
        {
            policy
                .WithOrigins(CorsAllowedDomains.DefaultDomain)
                .AllowAnyMethod()
                .AllowCredentials();
        }
    );
});
builder.Services.AddRequestTimeouts(options =>
{
    options.DefaultPolicy = new RequestTimeoutPolicy { Timeout = TimeSpan.FromSeconds(30) };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors(CorsPolicies.DefaultPolicy);
app.UseAuthorization();

app.UseAntiforgery();
app.MapEndpoints();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BibliotecaUteco.Client._Imports).Assembly);

app.Run();
