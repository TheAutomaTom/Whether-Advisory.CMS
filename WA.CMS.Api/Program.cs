using Serilog;
using WA.CMS.Domain.Config;

namespace WA.Api
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);
      var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
      var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env}.json", optional: true
        ).Build();

      // Add services to the container.

      builder.Services.AddControllers();

      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      // Logger
      builder.Services.ConfigureLogging(config, env ??= "Local");
      builder.Host.UseSerilog();


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
    }
  }
}
