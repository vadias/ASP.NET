
using AppAuthLesson4.Abstractions;
using AppAuthLesson4.Contexts;
using AppAuthLesson4.GraphQLApi;
using AppAuthLesson4.Securities;
using AppAuthLesson4.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AppAuthLesson4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AuthContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")


                    ?? throw new Exception("SqlServer not found in configuration"));
            });


            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IEncryptService, EncryptService>();

            builder.Services.AddSwaggerGen(
                        options =>
                        {
                            options.AddSecurityDefinition(
                                JwtBearerDefaults.AuthenticationScheme,
                                new()
                                {
                                    In = ParameterLocation.Header,
                                    Description = "Please insert JWT with Bearer into field",
                                    Name = "Authorization",
                                    Type = SecuritySchemeType.Http,
                                    BearerFormat = "Jwt Token",
                                    Scheme = JwtBearerDefaults.AuthenticationScheme
                                });

                            options.AddSecurityRequirement(new() {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new List<string>()
                    }
                            });
                        }
                    );

            var jwt = builder.Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>()
                ?? throw new Exception("JwtConfiguration not found");

            builder.Services.AddSingleton(provider => jwt);

            builder.Services.AddAuthorization();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                       .AddJwtBearer(options =>
                       {
                           options.TokenValidationParameters = new()
                           {
                               ValidateIssuer = true,
                               ValidateAudience = true,
                               ValidateLifetime = true,
                               ValidateIssuerSigningKey = true,
                               ValidIssuer = jwt.Issuer,
                               ValidAudience = jwt.Audience,
                               IssuerSigningKey = jwt.GetSigningKey()
                           };
                       });



            builder.Services.AddControllers();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}