using Serilog;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tutorial_WebApiBasic.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Tutorial_WebApiBasic.Injectables;

// try catch �� ������� �ʰ� Serilog �� ����Ͽ� ���ܸ� ó���Ѵ�.
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

    // 3. ������ ����(DI) - Scrutor����Ͽ� 
    // IInjectableService�� (Transient, Scoped, Singleton) �������̽��� ��ӹ��� Ŭ������ �����Ѵ�.
    builder.Services.Scan(scan => scan
            //.FromCallingAssembly()
            .FromAssemblies(typeof(Program).Assembly)
            //.FromAssemblies(AssemblyHelper.GetAllAssemblies(SearchOption.TopDirectoryOnly))
            //.FromAssemblyOf<ITransientService>()
            .AddClasses(classes => classes.AssignableTo<ITransientService>())
            .AsImplementedInterfaces()
            .WithTransientLifetime() // Transient
            //.FromAssemblyOf<IScopedService>() 
            .AddClasses(classes => classes.AssignableTo<IScopedService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime() // Scoped
            //.FromAssemblyOf<ISingletonService>()
            .AddClasses(classes => classes.AssignableTo<ISingletonService>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime() // Singleton
    );

var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();
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