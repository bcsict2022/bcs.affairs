using infrastructure.bcs.affairs.Entities;
using infrastructure.bcs.affairs.Factories;
using infrastructure.bcs.affairs.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AffairsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("basedConnection"),
        sqlServerOptionsAction: sql =>
        {
            sql.EnableRetryOnFailure(maxRetryCount: 6, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        });
});

builder.Services.RegisterRepos();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



public static class ServiceExtensions
{
    public static void RegisterRepos(this IServiceCollection collection)
    {
        collection.AddTransient<ISetupRegistrationTables, FactorySetupRegistrationTables>();
        collection.AddTransient<ISetupManagerTables, FactorySetupManagerTables>();
        collection.AddTransient<IBethels, FactoryBethels>();
        collection.AddTransient<IAffairsNavigations, FactoryAffairsNavigations>();
        collection.AddTransient<IUsers, FactoryUsers>();
    }
}