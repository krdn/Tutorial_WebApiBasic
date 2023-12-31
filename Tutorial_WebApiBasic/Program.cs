using Serilog;
using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tutorial_WebApiBasic.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Tutorial_WebApiBasic.Behaviors;
using Tutorial_WebApiBasic.Injectables;
using Tutorial_WebApiBasic.Middleware;
using Tutorial_WebApiBasic.Extensions;

// try catch 를 사용하지 않고 Serilog 를 사용하여 예외를 처리한다.
//var configuration = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json", false, true)
//    .AddJsonFile("appsettings.Development.json", optional: true)
//    .Build();

//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(configuration)
//    .CreateBootstrapLogger();

//try
//{

var builder = WebApplication.CreateBuilder(args);
    builder.Services.InitControllerAndSwagger();
    // https://learn.microsoft.com/ko-kr/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-7.0
    builder.Services.AddSerilog();
    builder.Services.InitMediatR();
    builder.Services.AddAccessDb(builder.Configuration.GetConnectionString("DefaultConnection"));
    builder.Services.InitScrutor();

var app = builder.Build();
    app.UseSwaggerAndSwaggerUI();
    app.UseMiddleware<ApiLoggingMiddleware>();
    app.MapControllers();

    // https://learn.microsoft.com/ko-kr/aspnet/core/mvc/controllers/routing?view=aspnetcore-7.0
    //app.MapControllerRoute(
    //    name: "default",
    //    pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
//}
//catch (Exception ex)
//{
//    string type = ex.GetType().Name;
//    if (type.Equals("StopTheHostException", StringComparison.OrdinalIgnoreCase)) throw;
//    Log.Fatal(ex, "Host terminated unexpectedly");
//}
//finally
//{
//    Log.CloseAndFlush();
//}