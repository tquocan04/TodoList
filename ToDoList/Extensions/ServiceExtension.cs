using Repositories.Interfaces;
using Repositories.Repositories;
using Services.Interfaces;
using Services.Services;

namespace ToDoList.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IItemService, ItemService>();

            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IItemRepository, ItemRepository>();

            return services;
        }
    }
}
