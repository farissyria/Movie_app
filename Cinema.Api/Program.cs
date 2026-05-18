using Cinema.Application.Mappings;
using Cinema.Application.Services;
using Cinema.Core.Entities;
using Cinema.Core.Interfaces;
using Cinema.Infrastructure;
using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Cinema.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ==========================================
            // 1. Add Services to Container
            // ==========================================
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // ✅ Add SwaggerGen with FULL configuration
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Cinema API",
                    Version = "v1",
                    Description = "Cinema Management System API with MongoDB"
                });

                // Add JWT support to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by a space and your JWT token"
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
                        Array.Empty<string>()
                    }
                });
            });

            // ==========================================
            // 2. Configure MongoDB
            // ==========================================
            builder.Services.AddSingleton<MongoDbContext>();

            // ==========================================
            // 3. Configure Identity with MongoDB
            // ==========================================
            builder.Services.AddIdentity<User, Roles>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddMongoDbStores<User, Roles, Guid>
            (
                builder.Configuration.GetConnectionString("MongoDBConnection") ?? "mongodb://localhost:27017",
                builder.Configuration["MongoDB:DatabaseName"] ?? "CinemaDB"
            )
            .AddDefaultTokenProviders();

            // ==========================================
            // 4. Configure JWT Authentication
            // ==========================================
            var jwtKey = builder.Configuration["Jwt:Key"] ?? "your-super-secret-key-with-at-least-32-characters-long";
            var jwtIssuer = builder.Configuration["Jwt:Issuer"];
            var jwtAudience = builder.Configuration["Jwt:Audience"];

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });
            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis");
                options.InstanceName = "Cinema_";
            });

            // Register cache service
            builder.Services.AddScoped<ICacheService, RedisCacheService>();

            // ==========================================
            // 5. Register Dependency Injection
            // ==========================================
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
            builder.Services.AddScoped<DbSeeder>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddMediatR(typeof(Cinema.Application.DTOs.MovieDto).Assembly);
           
         
            // ==========================================
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // ==========================================
            // 7. Build the App
            // ==========================================
            var app = builder.Build();



            // Quick test to create database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var seeder = services.GetRequiredService<DbSeeder>();
                    await seeder.SeedAllAsync();
                    Console.WriteLine("✅ Database seeding completed!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error seeding database: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                }
            }

            // ==========================================
            // 8. Configure HTTP Pipeline
            // ==========================================
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cinema API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}