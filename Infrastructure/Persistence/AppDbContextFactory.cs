using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public class AppDbContextFactory
        : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "../Case.Api"
                ))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")
                )
                .Options;

            return new AppDbContext(options);
        }
    }
}

//TO DO: Old version with the new one above is not working
//using Infrastructure.Persistence;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;

//namespace Infrastructure.Persistence
//{
//    public class AppDbContextFactory
//        : IDesignTimeDbContextFactory<AppDbContext>
//    {
//        public AppDbContext CreateDbContext(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

//            optionsBuilder.UseNpgsql(
//                "Host=localhost;Port=5432;Database=BancoPAN;Username=postgres;Password=123"
//            );

//            return new AppDbContext(optionsBuilder.Options);
//        }
//    }
//}
