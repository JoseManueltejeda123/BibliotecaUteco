using BibliotecaUteco;
using BibliotecaUteco.Components;
using BibliotecaUteco.Dependencies;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.Extensions.FileProviders;

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
                .AllowAnyHeader()
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
var logger = app.Services.GetRequiredService<ILogger<Program>>();
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;

        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature is not null)
        {
            logger.LogCritical($"Server Error: {contextFeature.Error.Message}");

            await context.Response.WriteAsJsonAsync(
                ApiResult<object>.BuildFailure(HttpStatus.Unauthorized,"Ocurrió un error en el servidor")

            );
        }
    });
});

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(app.Environment.WebRootPath, "BookCovers")),
    RequestPath = "/BookCovers",
    OnPrepareResponse = ctx =>
    {
        // Cache por 7 días
        ctx.Context.Response.Headers.Append(
            "Cache-Control", "public,max-age=604800");
    }
});
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
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<IBibliotecaUtecoDbContext>();    
    context.Database.Migrate();
}
app.Run();
