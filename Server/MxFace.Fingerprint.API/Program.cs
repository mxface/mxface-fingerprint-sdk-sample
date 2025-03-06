using Microsoft.OpenApi.Models;
using MxFace.SDK.Fingerprint;
using MxFace.SDK.Fingerprint.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "MxFace Fingerprint API",
            Version = "v1",
            Description = "API for fingerprint enrollment, search and verification",
        }
    );
});

builder.Services.AddHttpClient();

// Configure MxFace SDK
builder.UseMxFaceFingerprintSDK(configure: config =>
{
    config.Settings = new BiometricConfigurationSettings();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
