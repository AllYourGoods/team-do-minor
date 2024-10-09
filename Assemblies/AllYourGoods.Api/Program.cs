using System.Text;
using AllYourGoods.Api.Data;
using AllYourGoods.Api.Extensions;
using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Interfaces.Services;
using AllYourGoods.Api.Mappings;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Repositories;
using AllYourGoods.Api.Services;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
        app.UseAuthentication();
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
        builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        builder.Services.AddScoped<IRestaurantService, RestaurantService>();

        builder.Services.AddControllers();
        builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")));

        builder.Services.AddHealthChecks()
            .AddDbContextCheck<ApplicationContext>();

        // builder.Services.AddIdentity<User, IdentityRole>()
        builder.Services.AddIdentity<User, IdentityRole>(options => { options.SignIn.RequireConfirmedEmail = false; })
        .AddEntityFrameworkStores<ApplicationContext>()
        .AddDefaultTokenProviders();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });

        builder.Services.AddHealthChecks();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        // builder.Services.AddSwaggerGen();
        builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AllYourGoods.Api", Version = "v1" });
                
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

        return builder;
    }

    private static async Task InitializeDatabase(IHost app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<ApplicationContext>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await DbInitializer.Initialize(context, roleManager);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occured seeding the DB.");
        }
    }
}
