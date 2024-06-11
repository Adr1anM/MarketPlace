using MarketPlace.Infrastructure.DataSeed;
using MarketPlace.WebUI.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServices();


var app = builder.Build();

await app.SeedData();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCustomExeptionHandling();
app.UseTiming();
app.UseRouting();
app.UseCors(configurePolicy => configurePolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
      
public partial class Program;
