using MarketPlace.WebUI.Extentions;

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
app.UseTiming();
app.UseAuthorization();
app.MapControllers();
app.Run();
      
public partial class Program;
