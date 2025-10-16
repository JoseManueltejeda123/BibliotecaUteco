using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BibliotecaUteco.Dependencies;

public static class DbContextDependencies
{
    public static IServiceCollection AddBibliotecaUtecoDbContextServices(this IServiceCollection services, IConfiguration configuration
    )
    {
        services.AddDbContext<IBibliotecaUtecoDbContext, BibliotecaUtecoDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("MSSQL"));
            options.ConfigureWarnings(warning =>
                warning.Ignore(RelationalEventId.PendingModelChangesWarning)
            );
        });

        return services;
    }
}