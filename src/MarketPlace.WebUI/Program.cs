//using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.FileServices;
using MarketPlace.Infrastructure;
using MarketPlace.Infrastructure.FileSystem;
using MarketPlace.WebUI.Extentions;
//using MarketPlace.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServices();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IFileLogger, FileLogger>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


/*builder.Services.AddSingleton<IPaintRepository,PaintRepository>();
builder.Services.AddSingleton<ISculptureRepository,SculptureRepository>();
builder.Services.AddSingleton<IPhotographyRepository,PhotographyRepository>();*/
/*builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(IPhotographyRepository).Assembly));*/
builder.Services.AddHostedService<FileLoggingBackgroundService>();


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
