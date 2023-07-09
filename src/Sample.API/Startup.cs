namespace Sample.API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.RegisterServices(Configuration);
    }

    public void Configure(WebApplication app)
    {
        IWebHostEnvironment env = app.Environment;

        app.RegisterWebApp(env);
    }
}