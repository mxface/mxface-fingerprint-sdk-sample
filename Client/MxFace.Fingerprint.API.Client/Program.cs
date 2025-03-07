using MxFace.Fingerprint.API.Client.Components;
using MxFace.Fingerprint.API.Client.Interfaces;
using MxFace.Fingerprint.API.Client.Services;
using System.Net.Http.Headers;

namespace MxFace.Fingerprint.API.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddHttpClient<FingerprintApiService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7103/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true

            });

            // Configure device services
            builder.Services.AddHttpClient<DeviceService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:8034/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true

            });

            builder.Services.AddHttpClient<FingerprintCapturingService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:8034/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });

            // Register services
            builder.Services.AddScoped<ICapture, FingerprintCapturingService>();
            builder.Services.AddScoped<IDevice, DeviceService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
