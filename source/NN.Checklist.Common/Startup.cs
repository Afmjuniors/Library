using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NN.Checklist.Domain.Services;
using NN.Checklist.Domain.Services.Specifications;
using System.Text;
using TDCore.Core.Logging;

namespace NN.Checklist.Common
{
    public static class Startup
    {

        private static readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        private const string token = "8324590A&*B&*(3@C78((JFUHKJHfsgdiewok|:~][ioKfçskjf#@kkkjftijH&¨";

        private static void SetupBuilder(WebApplicationBuilder builder, string origins, string apiVersion, string apiName, 
            string apiTitle, string apiDescription)
        {
            var key = Encoding.UTF8.GetBytes(token);

            
                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                        };
                    });
            



            //CORS Configuration
            if (origins == null)
            {
                throw new Exception("CORS_Origins não encontrado no appsettings.json");
            }

            if (origins != null && origins.Trim().Length > 0)
            {
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy(MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod();
                    });
                });
            }
            else
            {
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy(MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
                });
            }
            builder.Services.AddControllers();


            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(apiName, new OpenApiInfo
                {
                    Version = apiVersion,
                    Title = apiTitle,
                    Description = apiDescription
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new List<string>()
                        }
                    });
            });
        }

        public static WebApplication Start(WebApplicationBuilder builder, bool checkDatabase, string apiVersion, string apiName, string apiTitle, string apiDescription, bool startMenuCache)
        {
            var origins = builder.Configuration.GetSection("CORS_Origins").Value;

            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs")))
            {
                // Se não existir, cria o diretório
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"));
            }
            if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "parameters")))
            {
                // Se não existir, cria o diretório
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "parameters"));
            }
            string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", $"{AppDomain.CurrentDomain.FriendlyName}.txt");
            string pathResources = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources");

            SetupBuilder(builder, origins, apiVersion, apiName, apiTitle, apiDescription);

            try
            {
                InitializationService.InitializeApplication(checkDatabase, logFile, pathResources, origins, token, startMenuCache).Wait();
            }
            catch (Exception ex)
            {
                return null;
            }

            var logger = TDCore.DependencyInjection.ObjectFactory.GetSingleton<ILog>();

            if (logger != null)
            {
                logger.Log(LogType.Information, "error", "Api started!");
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/{apiName}/swagger.json", apiDescription);
                });

                app.UseDeveloperExceptionPage();
                app.Use(async (context, next) =>
                {
                    Console.WriteLine($"-------------- BEGIN OF HEADER ---------------");
                    Console.WriteLine($"REQUEST: {context.Request.Path}");
                    Console.WriteLine(context.Request.Body);
                    foreach (var header in context.Request.Headers)
                    {
                        Console.WriteLine($"{header.Key}:{header.Value.ToString()}");
                    }
                    Console.WriteLine($"--------------  END OF HEADER  ---------------");
                    await next.Invoke();
                });
            }


            app.UseCors(MyAllowSpecificOrigins);

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
