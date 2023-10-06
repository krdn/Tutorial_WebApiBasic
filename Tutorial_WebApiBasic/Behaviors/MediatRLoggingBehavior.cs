using MediatR;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Tutorial_WebApiBasic.Injectables;
using Tutorial_WebApiBasic.Logger;

namespace Tutorial_WebApiBasic.Behaviors;



/// <summary>
/// https://www.youtube.com/watch?v=fx7MNgsNCrM
///
/// Program.cs에서 아래 코드 삭제하여도됨.
/// builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MediatRLoggingBehavior<,>));
///
/// API 호출시 로그를 남기는 MediatR Pipeline Behavior
/// 
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IMediatRLoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>, ITransientService
    where TRequest : IRequest<TResponse>
{ }

public class MediatRLoggingBehavior<TRequest, TResponse>
    : IMediatRLoggingBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IApiLogger _logger;

    public MediatRLoggingBehavior(IApiLogger logger)
    {
        _logger = logger;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        string uniqueId = Guid.NewGuid().ToString();
        string requestMessage = JsonSerializer.Serialize(request,
            new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            });

        _logger.LogInformation($"Begin Request Id:{uniqueId}, " +
                               $"request name:{requestName},\nRequest={requestMessage}");

        var timer = new Stopwatch();
        timer.Start();
        var response = next();
        timer.Stop();

        string responseMessage = JsonSerializer.Serialize(response.Result,
            new JsonSerializerOptions
            {
                WriteIndented = true,
                IncludeFields = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            });

        _logger.LogInformation($"End Request Id:{uniqueId}, " +
                               $"request name:{requestName},\nResponse={responseMessage}" +
                               $"\ntotal elapsed time: {timer.ElapsedMilliseconds}");

        return response;
    }

}