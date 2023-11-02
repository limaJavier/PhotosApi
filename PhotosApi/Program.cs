using Microsoft.EntityFrameworkCore;
using PhotosApi.Repositories;
using PhotosApi.Services;

var builder = WebApplication.CreateBuilder(args);
{
    var config = builder.Configuration;
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<PhotosDBContext>(options =>
    {
        options.UseSqlServer(config["ConnectionStrings:PhotosDBConnection"]);
    });


    builder.Services.AddTransient<IPhotosRepository, PhotosRepository>();
    builder.Services.AddScoped<IPhotosService, PhotosService>();
    builder.Services.AddScoped<IStorageService, StorageService>();
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}