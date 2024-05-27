using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using StudentWebAPI.Configuration;
using StudentWebAPI.Controllers;
using StudentWebAPI.Models;

namespace StudentWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("StudentAppConnectionString");

            builder.Services.AddDbContext<StudentAppContext>(options => options.UseSqlite(connectionString));

            builder.Services.AddControllers();

            builder.Services.AddAuthentication(
                options =>
                {
                    options.DefaultScheme = "ApiKeySchema";
                    options.DefaultChallengeScheme = "ApiKeySchema";
                })
                .AddScheme<CustomAuthenticationOptions, CustomAuthHandler>("ApiKeySchema", null);

            builder.Services.AddAuthorization();

            // Add Automapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MuhimbiAPI", Version = "v1" });
                    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
                    {
                        In = ParameterLocation.Header,
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
                        },
                        new string[] { }
                    }
                });
                }




                );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}