using Serilog;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tutorial_WebApiBasic.Infrastructure;
using Microsoft.AspNetCore.Builder;

// try catch 를 사용하지 않고 Serilog 를 사용하여 예외를 처리한다.
//var configuration = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json", false, true)
//    .AddJsonFile("appsettings.Development.json", optional: true)
//    .Build();

//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(configuration)
//    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// https://learn.microsoft.com/ko-kr/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-7.0
builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
