using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.CutomResponses;
using System.Text;
using System.Text.Json;

namespace E_Commerce.Web.Extentions;

public static class ServicesExtensions
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen();
        //options =>
        //{
        //    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //    {
        //        In = ParameterLocation.Header,
        //        Name = "Authorization",
        //        Type = SecuritySchemeType.Http,
        //        Scheme = "Bearer",
        //        BearerFormat = "JWT",
        //        Description = "Enter 'Bearer' followed by a space and your token."
        //    });

        //    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        //    {
        //        {
        //            new OpenApiSecurityScheme
        //            {
        //                Reference = new OpenApiReference
        //                {
        //                    Id = "Bearer",
        //                    Type = ReferenceType.SecurityScheme
        //                }
        //            },
        //            Array.Empty<string>()
        //        }
        //    });
        //});
        return services;
    }
    public static IServiceCollection AddWebApplicationServices(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>((options) =>
        {
            options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateValidationResponse;
        });

        return services;
    }

    public static IServiceCollection AddJWTServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(config =>
        {
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["JWTOptions:Issuer"],

                ValidateAudience = true,
                ValidAudience = configuration["JWTOptions:Audience"],
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTOptions:Key"]!)),
                ClockSkew = TimeSpan.Zero
            };
            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var response = new CustomError
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Message = "Unauthorized"
                    };

                    await context.Response.WriteAsJsonAsync(
                        response,
                        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
            };
        });
        return services;
    }
}
