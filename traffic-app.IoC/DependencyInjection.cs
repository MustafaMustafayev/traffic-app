using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using traffic_app.BLL.Services;
using traffic_app.BLL.Services.IServices;
using traffic_app.Core.Utility;
using traffic_app.DAL.DatabaseContext;
using traffic_app.DAL.Repositories;
using traffic_app.DAL.Repositories.IRepositories;

namespace traffic_app.IoC
{
    public class DependencyInjection
    {
        private readonly IConfiguration Configuration;

        public DependencyInjection(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void InjectDependencies(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<TrafficDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TrafficDB"));
            });

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();

            services.AddScoped<IPostImageRepository, PostImageRepository>();
            services.AddScoped<IPostImageService, PostImageService>();

            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();

            services.AddScoped<IUtil, Util>();
            
            services.AddScoped<IUserElasticsearchService, UserElasticsearchService>();

            services.AddScoped<IOnTheWayDriverPostRepository, OnTheWayDriverPostRepository>();
            services.AddScoped<IOnTheWayDriverPostService, OnTheWayDriverPostService>();

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("JWTSettings:SecretKey").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrafficApp API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                            new string[] {}
                    }
                });
            });
        }
    }
}
