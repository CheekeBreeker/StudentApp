using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using StudentMvc.Config;
using StudentMvc.Services;

namespace StudentMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var apiConfig = builder.Configuration.GetSection("ApiConfig").Get<ApiConfig>();

            builder.Services.AddHttpClient("WebApiServer", httpClient =>
            {
                httpClient.BaseAddress = new Uri(apiConfig.Host);
                httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Token {apiConfig.Token}");
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/login";
                });

            builder.Services.AddAuthorization();

            builder.Services.Configure<ApiConfig>(builder.Configuration.GetSection("ApiConfig"));

            builder.Services.AddScoped<HttpClientService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Student}/{action=Index}/{id?}");

            app.Run();
        }
    }
}