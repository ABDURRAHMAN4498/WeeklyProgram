using Microsoft.EntityFrameworkCore;
using WeeklyProgram.Data;

namespace WeeklyProgram.Extensions
{
    public static class ApplicationExtension
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
            {
            
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));   
            });
        }
    }
}