using GodsOfStruggle.API.Extensions;
using GodsOfStruggle.API.Middleware;
using GodsOfStruggle.Application.DependencyInjection;
using GodsOfStruggle.Infrastructure.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerWithJwt();
builder.Services.AddCors(options =>
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()));

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("AllowFrontend");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
