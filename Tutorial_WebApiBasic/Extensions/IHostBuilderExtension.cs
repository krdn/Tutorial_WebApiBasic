using Serilog;

namespace Tutorial_WebApiBasic.Extensions;

public static class IHostBuilderExtension
{
    public static void AddSerilog(this IHostBuilder self)
    {
        self.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));
    }
}