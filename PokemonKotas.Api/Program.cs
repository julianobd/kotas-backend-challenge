using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PokemonKotas.Data.Context;
using PokemonKotas.Data.Repositories;
using PokemonKotas.Domain.Interfaces;
using PokemonKotas.Infra.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddHttpClient();

builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://localhost:7013", "https://front-pokemon.deiro.dev.br")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
//context
builder.Services.AddDbContext<MasterPokemonDbContext>();

//services
builder.Services.AddScoped<IMasterPokemonService, MasterPokemonService>();

//repositories
builder.Services.AddScoped<IMasterPokemonRepository, MasterPokemonRepository>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<MasterPokemonDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error to apply migrations: {ex.Message}");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowSpecificOrigins");
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.Run();