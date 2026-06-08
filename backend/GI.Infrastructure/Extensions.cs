using FluentValidation;
using GI.Application.Common.Interfaces;
using GI.Application.Common.Mappings;
using GI.Infrastructure.Data;
using GI.Infrastructure.Services;
using GI.Web.Behaviors;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace GI.Infrastructure
{
    public static class Extensions
    {
        public static void InitializeAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            
        }

        public static void ConfigureDefaults(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAppDbContext>(sp => sp.GetRequiredService<AppDbContext>());
            ConfigureServices(services);
            ConfigureJwt(services, configuration);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GI.Application.Features.Auth.Commands.Register.RegisterCommand).Assembly));
            services.AddValidatorsFromAssembly(typeof(GI.Application.Features.Auth.Commands.Register.RegisterCommand).Assembly);
            ConfigureValidator(services);
            services.AddAutoMapper(x => x.AddProfile(typeof(MappingProfile)));
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
        }

        private static void ConfigureValidator(IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        }

        private static void ConfigureJwt(IServiceCollection services, IConfiguration configuration)
        {
            var jwt = configuration.GetSection("JwtSettings");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwt["Issuer"],
                        ValidAudience = jwt["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwt["SecretKey"]!)),

                        ClockSkew = TimeSpan.Zero //Without this there will be extra 5 mins grace time will be added to the token
                    };
                });
        }
    }
}
