using Sample.API.BusinessLayer.Logging;
using Sample.API.BusinessLayer.Service;
using Sample.API.BusinessLayer.Validation.Validator;
using Sample.API.DataAccessLayer.Infrastructure;

namespace Sample.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers().AddSimpleJsonOptions();
        services.AddProblemDetails();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGenConfig("Sample API", "v1", string.Empty);

        var connectionString = configuration.GetSection("ConnectionStrings").GetValue<string>("Default");

        services.AddDbContextServicesGenerics<DataDbContext>();
        services.AddDbContextForSQLite<DataDbContext>(connectionString, string.Empty);

        services.AddTransient<IPeopleService, PeopleService>();
        services.AddValidatorsFromAssemblyContaining<PersonValidator>();

        services.AddSerilogSeqServices();
        services.AddHealthChecksUISQLite<DataDbContext>(connectionString);

        return services;
    }

    public static WebApplication RegisterWebApp(this WebApplication app, IWebHostEnvironment env)
    {
        app.UseProblemDetails();
        app.UseHttpsRedirection();

        LoggerService.Init(app.Services.GetRequiredService<ILoggerFactory>());
        app.AddSerilogConfigureServices();

        if (env.IsDevelopment())
        {
            app.UseSwaggerUI("Sample API");
        }

        app.UseRouting();
        app.UseHealthChecksUI();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
}
