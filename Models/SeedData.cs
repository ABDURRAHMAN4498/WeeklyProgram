using Microsoft.EntityFrameworkCore;
using WeeklyProgram.Data;
using WeeklyProgram.Models;

namespace WeeklyProgram
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationContext(
                serviceProvider.GetRequiredService<
                DbContextOptions<ApplicationContext>>()))
            {
                if (context.Categories.Any())
                {
                    return;
                }
                var categories = new List<Category>
            {
                new Category { Name = "مدرسي" },
                new Category {  Name = "جداول الدروس", ParentCategoryId = 6 },
                new Category {  Name = "شهادات مدرسية", ParentCategoryId = 6 },
                new Category {  Name = "فواتير" },
                new Category {  Name = "فاتورة كهرباء", ParentCategoryId = 9 },
                new Category {  Name = "فاتورة مياه", ParentCategoryId = 9 },
                new Category {  Name = "شهادة اعدادي", ParentCategoryId = 8 },
                new Category {  Name = "تصاميم" }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();
            }
        }
    }
}