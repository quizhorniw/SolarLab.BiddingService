using Scalar.AspNetCore;
using SolarLab.BiddingService.Hosts.PublicApi.ExceptionHandlers;
using SolarLab.BiddingService.Hosts.PublicApi.Scalar.DocumentTransformers;
using SolarLab.BiddingService.Infrastructure.ComponentRegistrar;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<OpenApiInfoDocumentTransformer>();
});

builder.Services.RegisterApplicationServices(builder.Configuration);

builder.Services.AddSignalR();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Theme = ScalarTheme.DeepSpace;
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();