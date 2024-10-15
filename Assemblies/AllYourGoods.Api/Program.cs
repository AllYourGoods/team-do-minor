using AllYourGoods.Api.Data;
using AllYourGoods.Api.Extensions;
using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Mappings;
using AllYourGoods.Api.Repositories;
using AllYourGoods.Api.Services;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;

namespace AllYourGoods.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = CreateBuilder(args);
        var app = builder.Build();

        InitializeDatabase(app);

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "AllYourGoods API V1");
                options.RoutePrefix = "";
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.EnableBuffering();
        app.EnableRequestBodyLoggingMiddleware();

        app.MapControllers();
        app.MapHealthChecks("/health");
        app.Run();
    }

    private static WebApplicationBuilder CreateBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (!builder.Environment.IsDevelopment())
        {
            var keyVaultUri = builder.Configuration["KEY_VAULT_URI"];
            builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri!), new DefaultAzureCredential());
        }

        builder.Services.AddApplicationInsightsTelemetry(options =>
        {
            options.EnableAdaptiveSampling = false;
        });

        builder.Logging.AddApplicationInsights();

        // Add services to the container.
        builder.Services.AddAutoMapper(typeof(RestaurantMappingProfile));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddScoped<IRestaurantService, RestaurantService>();

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                options.JsonSerializerOptions.MaxDepth = 64; 
            });
        builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")));

        builder.Services.AddHealthChecks()
            .AddDbContextCheck<ApplicationContext>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }

    private static void InitializeDatabase(IHost app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ApplicationContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occured seeding the DB.");
        }
    }
}
