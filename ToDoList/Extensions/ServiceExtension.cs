using Repositories.Interfaces;
using Repositories.Repositories;
using Services.Interfaces;
using Services.Services;
using TodoItem.Services;

namespace ToDoList.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<JwtService>();

            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();

            return services;
        }
    }
}
