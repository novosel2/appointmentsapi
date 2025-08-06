using Api.Filters;
using Application.IRepositories;
using Application.IServices;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.StartupExtensions;

public static class ConfigureServicesExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(opt =>
        {
            opt.Filters.Add(typeof(HandleExceptionsFilter));
        });
        services.AddSwaggerGen();

        services.AddDbContext<AppDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("AppConnectionString")));

        // Register repositories
        services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();

        // Register services
        services.AddScoped<IAppointmentsService, AppointmentsService>();

        return services;
    }
}
