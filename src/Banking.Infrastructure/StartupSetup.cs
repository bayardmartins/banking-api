using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Banking.Infrastructure.Data;

namespace Banking.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<BankingContext>(
                options =>
                    options.UseNpgsql(connectionString,
                    x => x.MigrationsAssembly("Banking.Infrastructure")));
    }
}