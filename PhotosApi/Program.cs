using Microsoft.EntityFrameworkCore;
using PhotosApi.Models.PhotoFile;
using PhotosApi.Models.PhotoModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PhotoModelDbContext>(opts => {
    opts.UseSqlServer(
    builder.Configuration["ConnectionStrings:PhotosDBConnection"]);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPhotoModelRepository, PhotoModelRepository>();
builder.Services.AddSingleton<IPhotoFileRepository, PhotoFileRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
