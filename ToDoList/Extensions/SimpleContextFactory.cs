using Datas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TodoItem.Extensions
{
    public class SimpleContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<Context>();
            var connectionString = configuration.GetConnectionString("DBConnection");
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("TodoItem"));

            return new Context(builder.Options);
        }
    }
}
