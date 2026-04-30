using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RecipesApi.Components;
using RecipesApi.Data;
using RecipesApi.Services;

namespace RecipesApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IConfiguration Configuration = builder.Configuration;
        
        string connectionString = (Configuration.GetConnectionString("DefaultConnection")
                                   ?? Environment.GetEnvironmentVariable("DefaultConnection"))!;

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddDbContext<AppDBContext>(options =>
            options.UseNpgsql(connectionString));
        
        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddHttpClient<ApiService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7093/api/");
            Console.WriteLine($"APIService BaseAddress: {client.BaseAddress}");
        });
        
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowSpecificOrigins",
                policyBuilder =>
                {
                    policyBuilder
                        .WithOrigins(
                            Configuration["Origin"],
                            "https://localhost:5273",
                            "https://localhost:8080",
                            "https://localhost:7231"
                        )
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Content-Disposition");
                }
            );
        });
        
        var app = builder.Build();
        
        app.UseCors("AllowSpecificOrigins");
        
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        app.UseHttpsRedirection();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
        });
        
        app.UseAntiforgery();
        app.MapControllers();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}