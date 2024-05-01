//using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions;
using MarketPlace.Infrastructure;
using MarketPlace.Infrastructure.FileSystem;
using MarketPlace.WebUI.Extentions;
//using MarketPlace.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServices();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();   

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
