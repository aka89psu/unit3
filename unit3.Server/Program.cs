
using AspNetCoreRateLimit;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace unit3.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var settings = builder.Configuration.GetRequiredSection("Settings").Get<Settings>();

            var apiDatabase = settings?.ApiDatabase;
            var apiDatabaseType = settings?.ApiDatabaseType;

            builder.Services.AddSingleton(new Settings { ApiDatabase = apiDatabase, ApiDatabaseType = apiDatabaseType });

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            builder.Services.AddMemoryCache();
            builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
            builder.Services.AddInMemoryRateLimiting();
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            builder.Services.AddResponseCaching();

            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
            builder.Services.AddOcelot();

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenLocalhost(5000);


                options.ListenAnyIP(7173, listenOptions =>
                {
                    listenOptions.UseHttps();
                });

            });


            builder.Services.AddAuthentication("Bearer")
                            .AddJwtBearer("Bearer", options =>
                            {
                                options.Authority = "https://trial-7039807.okta.com/oauth2/default";
                                options.Audience = "0oav6th7polRMWkz9697";
                                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidateLifetime = true,
                                    ValidateIssuerSigningKey = true
                                };
                            });



            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseIpRateLimiting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseResponseCaching();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            app.UseOcelot();

            app.Run();
        }
    }
}
