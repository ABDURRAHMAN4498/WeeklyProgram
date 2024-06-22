using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeeklyProgram.Models;

namespace WeeklyProgram.Models
{
    public class Template
    {
        [Key]
        public Guid Id { get; set; } 

        [DataType(DataType.Text)]
        [Display(Name ="العنوان")]
        [Required(ErrorMessage ="العنوان ضروري!!")]
        public string? Title { get; set; }

        [Display(Name ="عدد الاسطر")]
        [Required(ErrorMessage ="عدد الاسطر ضروري!!")]
        public int ArrayRow { get; set; }

        [Display(Name ="عدد الاعمدة")]
        public int ArrayColmun { get; set; }
        [DataType(DataType.Text)]
        [Display(Name ="الوصف")]
        public string? Descreption { get; set; }

        [Display(Name ="الصورة")]
        public string? ImageUrl { get; set; }

        [DataType(DataType.Text)]
        public string? Objectstext { get; set; }

        [NotMapped]
        public string[]? ObjectJson { get; set; }
        [Display(Name ="الفئة")]
        public int CategoryId { get; set; }
        
        public Category? Category { get; set; }

        
    }
}