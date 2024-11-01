using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interfaces;
using Repositories.Repositories;
using Services.Interfaces;
using Services.Services;
using System.Text;

namespace ToDoList.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();

            return services;
        }
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSetting = configuration.GetSection("Jwt");
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }) .AddJwtBearer(options =>
               {
                    options.TokenValidationParameters = new TokenValidationParameters   //tham so xac thuc cho jwt
                    {
                        //cap token: true-> dich vu, false->tu cap
                        ValidateIssuer = true,
                        ValidIssuer = jwtSetting["Issuer"],

                        ValidateAudience = true,
                        ValidAudience = jwtSetting["Audience"],

                        ClockSkew = TimeSpan.Zero, // bo tg chenh lech
                        ValidateLifetime = true,    //xac thuc thoi gian ton tai cua token

                        //ky vao token
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting["Secret"])),
                        ValidateIssuerSigningKey = true
                    };
               });
        }
    }
}
