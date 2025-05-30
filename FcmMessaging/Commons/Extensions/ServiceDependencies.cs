using AutoMapper.Internal;
using FcmMessaging.Commons.Configurations;
using FcmMessaging.Commons.Mappings;
using FcmMessaging.Infrastructure.Persistence.Sql;
using FcmMessaging.Infrastructure.Service;
using FcmMessaging.Interfaces;
using FcmMessaging.Services;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Utilities.Filters;

namespace FcmMessaging.Commons.Extensions;

public static class ServiceDependencies
{
    public static TBuilder AddAutoMapper<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddAutoMapper(cfg => cfg.Internal().MethodMappingEnabled = false,
            typeof(MappingProfile).Assembly);
        return builder;
    }
    
    public static TBuilder InitiateFirebase<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        var firebaseSettings = builder.Configuration.GetSection("Firebase").Get<FirebaseConfiguration>();
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromJsonParameters(new JsonCredentialParameters()
            {
                ClientEmail = firebaseSettings.ClientEmail,
                ClientId = firebaseSettings.ClientId,
                ProjectId = firebaseSettings.ProjectId,
                PrivateKey = firebaseSettings.PrivateKey,
                PrivateKeyId = firebaseSettings.PrivateKeyId,
                TokenUrl = firebaseSettings.TokenUri,
                Type = firebaseSettings.Type,
                UniverseDomain = firebaseSettings.UniverseDomain
            }),
            ProjectId = firebaseSettings.ProjectId,
        });
        
        // Optional: Verify initialization
        var defaultInstance = FirebaseApp.DefaultInstance;
        if (defaultInstance == null)
        {
            throw new Exception("Firebase failed to initialize");
        }
        
        // Register Firebase services
        builder.Services.AddSingleton(FirebaseApp.DefaultInstance);
        builder.Services.AddSingleton(FirebaseMessaging.DefaultInstance);
        
        return builder;
    }

    public static TBuilder RegisterServices<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IFcmMessageService, FcmMessageService>();
        builder.Services.AddTransient<INotificationService, NotificationService>();
        builder.Services.AddTransient<IExpoSerivce, ExpoService>();
        
        return builder;
    }

    public static TBuilder AddSqliteDependency<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));
        
        return builder;
    }
    
    public static TBuilder AddSqlDependency<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        if (builder.Configuration.GetValue<bool>("UseInMemoryDatabase"))
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("FcmMessaging.ApplicationDbContext"));
        else
            builder.Services.AddDbContextFactory<ApplicationDbContext>(options => options
                .UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                .EnableSensitiveDataLogging());

        // // Register health check
        // builder.Services.AddHealthChecks()
        //     .AddSqlServer(connectionString: builder.Configuration.GetConnectionString("SqlConnection"));
        
        return builder;
    }
    
    public static TBuilder ConfigureSwaggerGen<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Custom Message Service",
                Version = "v1",
                Description = "A service for send custom messages"
            });
            c.EnableAnnotations();
            
            c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                In = ParameterLocation.Header,
                Scheme = "basic"

            });
            c.SchemaFilter<SwaggerSchemaExampleFilter>();
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="basic"
                        }
                    },
                    new string[]{}
                }
            });
        });

        return builder;
    }
}