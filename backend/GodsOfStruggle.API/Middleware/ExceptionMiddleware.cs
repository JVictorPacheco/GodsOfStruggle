using FluentValidation;
using GodsOfStruggle.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace GodsOfStruggle.API.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            var errors = ex.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage });
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { errors }, _jsonOptions));
        }
        catch (DomainException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = ex.Message }, _jsonOptions));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro não tratado");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = "Ocorreu um erro interno." }, _jsonOptions));
        }
    }
}
