using System.ComponentModel.DataAnnotations;
using WeeklyProgram.Models;

namespace WeeklyProgram.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "اسم الفئة")]
        public string? Name { get; set; }
        [Display(Name = "الفئة الاب")]
        public int? ParentCategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category>? SubCategories { get; set; }

    }
}