namespace Tutorial_WebApiBasic.Extensions;

public static class IApplicationBuilderExtension
{
    public static void UseSwaggerAndSwaggerUI(this IApplicationBuilder self)
    {
        self.UseSwagger();
        self.UseSwaggerUI();
    }
}
