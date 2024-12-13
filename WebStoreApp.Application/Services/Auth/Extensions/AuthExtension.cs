using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace WebStoreApp.Application.Services.Auth.Extension
{
    public static class AuthExtension
    {
        public static IServiceCollection AddAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<JwtProvider>();
            serviceCollection.Configure<AuthOptions>(configuration.GetSection("AuthOptions"));

            var authSettings = configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>();
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = authSettings.GetSymmetricSecurityKey()
                });
            return serviceCollection;
        }
    }
}
