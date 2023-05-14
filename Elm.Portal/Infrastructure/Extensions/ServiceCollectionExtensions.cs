namespace Appo.Server.Infrastructure
{
    using Appo.Server.Data;
    using Appo.Server.Features.Identity;
    using Appo.Server.Infrastructure.Extensions;
    using Appo.Server.Infrastructure.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Plugins.DataStore.SQL;
    using Plugins.DataStore.SQL.Generic;
    using Plugins.DataStore.SQL.Infrastructure.Services;
    using Portal.API.Features.Doctors.Service;
    using Portal.API.Helper;
    using System;
    using System.Text;
    using System.Text.Json.Serialization;
    using UseCases.DataStorePluginInterfaces;
    using UseCases.DataStorePluginInterfaces.Generic;
    public static class ServiceCollectionExtensions
    {

        public static AppSettings GetAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingConfigration = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingConfigration);
            return appSettingConfigration.Get<AppSettings>();
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {

            services.AddIdentity<IdentityUser, IdentityRole>(option
                    =>
            {
                option.Password.RequiredLength = 6;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;

            })
                .AddEntityFrameworkStores<AccountContext>();
            return services;
        }

        public static IServiceCollection AddAutoMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }


        public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, AppSettings appSetting)
        {

            var key = Encoding.ASCII.GetBytes(appSetting.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserServiceDB, CurrentUserServiceDB>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddTransient<IIdentityService, IdentityService>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IHelperSrv, HelperSrv>();
            return services;
        }



        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<PortalContext>(
                options =>
                     options.UseSqlServer(
                         configuration.GetDefaultConnectionString()))

            .AddDbContext<AccountContext>(
                options =>
                     options.UseSqlServer(
                         configuration.GetAccountConnectionString()));

        public static void AddApiController(this IServiceCollection services)
            => services
            .AddControllers()
            .AddControllersAsServices().AddJsonOptions(
                x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
                );

        public static IServiceCollection AddSwagger(this IServiceCollection services)
            => services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Portal.Server API",
                    Description = "Portal.Server ASP.NET Core Web API",
                    TermsOfService = new Uri("https://kashifabbas.dev"),
                    Contact = new OpenApiContact
                    {
                        Name = "Kashif Abbas",
                        Email = "me@kashifabbas.dev",
                        Url = new Uri("https://twitter.com/skashif18"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://kashifabbas.dev"),
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

            });

    }
}
