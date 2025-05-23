using Bugsnag;
using System.Net;
using System.Text.Json;

namespace MakaleYazariMvc.Middlewares;
/// <summary>
/// Hata yönetimi için özel middleware sınıfı
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }


    // HTTP isteği işlenmeden önce hata yakalama mekanizması eklenir ve hata durumunda loglama yapılır.
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);// İstek bir sonraki middleware'e yönlendirilir.
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Bir hata oluştu: {ex.Message}");// Hata loglanır.
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    // Hata yönetimi işlemi, hata detaylarını JSON formatında geri döndürür.
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Sunucuda bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.",
            Detail = exception.Message
        };

        var json = JsonSerializer.Serialize(response);// Yanıtı JSON formatına dönüştürür.
        return context.Response.WriteAsync(json);// JSON yanıtını döndürür.
    }
}
