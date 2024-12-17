using System.Reflection;
using System.Text;
using GenClinic_Api.ExtAuthorization;
using GenClinic_Common.Constants;
using GenClinic_Entities.DTOs.Request;
using GenClinic_Repository.Data;
using GenClinic_Repository.IRepositories;
using GenClinic_Repository.Repositories;
using GenClinic_Service.IServices;
using GenClinic_Service.Profiles;
using GenClinic_Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GenClinic_Api.Extensions
{
    public static class ApplicationBuilderConfiguration
    {
        public static void RegisterDatabaseConnection(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
        }

        public static void RegisterUnitOfWork(this IServiceCollection services) => services.AddScoped<IUnitOfWork, UnitOfWork>();

        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }

        public static void SetRequestBodySize(this IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            IEnumerable<Type> implementationTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseService<>)));

            foreach (Type implementationType in implementationTypes)
            {
                Type[] implementedInterfaces = implementationType.GetInterfaces();
                foreach (Type implementedInterface in implementedInterfaces)
                {
                    if (implementedInterface.IsGenericType)
                    {
                        Type openGenericInterface = implementedInterface.GetGenericTypeDefinition();
                        if (openGenericInterface == typeof(IBaseService<>))
                        {
                            services.AddScoped(implementedInterface, implementationType);
                        }
                    }
                }
            }

            services.Scan(scan =>
            {
                scan.FromAssembliesOf(typeof(IBaseService<>))
                    .AddClasses()
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });

            services.AddHttpContextAccessor();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                  );
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "GenClinic v1",
                    Version = "v1"
                });
            });
        }

        public static void RegisterMail(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MailSettingDto>(config.GetSection("MailSettings"));
            services.AddScoped<IMailService, MailService>();
        }

        public static void ConfigAuthentication(this IServiceCollection services, IConfiguration config)
        {

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                            }
                        },
                    new List < string > ()
                    }
                    });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? ""))
                    };
                });

            // custom auth filter
            services.AddScoped<ExtAuthorizeFilter>();


            // Register the ExtAuthorizeHandler before adding policies
            services.AddScoped<IAuthorizationHandler, ExtAuthorizeHandler>();

            services.AddAuthorization(config =>
        {
            config.AddPolicy(SystemConstants.DoctorPolicy, policy =>
            {
                policy.Requirements.Add(new ExtAuthorizeRequirement(SystemConstants.DoctorPolicy));
            });
            config.AddPolicy(SystemConstants.PatientPolicy, policy =>
            {
                policy.Requirements.Add(new ExtAuthorizeRequirement(SystemConstants.PatientPolicy));
            });
            config.AddPolicy(SystemConstants.LabUserPolicy, policy =>
            {
                policy.Requirements.Add(new ExtAuthorizeRequirement(SystemConstants.LabUserPolicy));
            });
            config.AddPolicy(SystemConstants.AllUserPolicy, policy =>
            {
                policy.Requirements.Add(new ExtAuthorizeRequirement(SystemConstants.AllUserPolicy));
            });
        });
        }
    }
}