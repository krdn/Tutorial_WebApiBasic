using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tutorial_WebApiBasic.Behaviors;
using Tutorial_WebApiBasic.Helper;
using Tutorial_WebApiBasic.Infrastructure;
using Tutorial_WebApiBasic.Injectables;

namespace Tutorial_WebApiBasic.Extensions;

public static class IServiceCollectionExtension
{
    public static void InitControllerAndSwagger(this IServiceCollection self)
    {
        self.AddControllers();
        self.AddEndpointsApiExplorer();
        self.AddSwaggerGen();
    }

    public static void InitMediatR(this IServiceCollection self)
    {
        //self.AddMediatR(AssemblyHelper.GetAllAssemblies().ToArray());

        self.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        self.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatRLoggingBehavior<,>));

    }

    public static void AddAccessDb(this IServiceCollection self, string? connectionString)
    {
        //self.AddDbContext<MyDbContext>(options => options.UseSqlServer(connectionString));
        self.AddDbContext<MyDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }

    public static void InitScrutor(this IServiceCollection self)
    {
        // 3. 의존성 주입(DI) - Scrutor사용하여 
        // IInjectableService에 (Transient, Scoped, Singleton) 인터페이스를 상속받은 클래스를 주입한다.
        self.Scan(scan => scan
            //.FromAssemblies(AssemblyHelper.GetAllAssemblies())
            .FromAssemblies(typeof(Program).Assembly)
            .AddClasses(classes => classes.AssignableTo<ITransientService>())
            .AsImplementedInterfaces()
            .WithTransientLifetime()
            .AddClasses(classes => classes.AssignableTo<IScopedService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            .AddClasses(classes => classes.AssignableTo<ISingletonService>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime()
        );
    }

}