using System.Text.Json.Serialization;
using Afro.API.src.BuildingBlocks.Authentication;
using Afro.API.src.BuildingBlocks.Storage;
using Afro.API.src.Infrastructure.Persistence;
using Afro.API.src.Infrastructure.Seed;
using Afro.API.src.Modules.Identity;
using Afro.API.src.Modules.Listings;
using Afro.API.src.Modules.Media;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure JSON enum serialization globally
builder.Services.ConfigureHttpJsonOptions(
    options =>
    {
        options.SerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    }
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedFrontendOrigins", policy =>
    {
        var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new string[0];
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();

    });
});


// Add services to the container.

builder.Services.AddOpenApi();

// Add modules/services
builder.Services.AddIdentityModule();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddStorageExtensions(builder.Configuration);
builder.Services.AddListingsModule();
builder.Services.AddMediaModule();

builder.Services.AddDatabase(builder.Configuration);



var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowedFrontendOrigins");
app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapIdentityEndpoints();
app.MapListingEndpoints();
app.MapMediaEndpoints();


// Seed data
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await db.Database.MigrateAsync();
await app.Services.SeedDatabaseAsync();

app.Run();


