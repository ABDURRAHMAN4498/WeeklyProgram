using System.ComponentModel.DataAnnotations;
using WeeklyProgram.Models;

namespace WeeklyProgram.ViewModel
{
    public class ProjectViewModel
    {
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "العنوان")]
        [Required(ErrorMessage = "العنوان ضروري!!")]
        public string? Title { get; set; }

        [Display(Name = "عدد الاسطر")]
        [Required(ErrorMessage = "عدد الاسطر ضروري!!")]
        public int ArrayRow { get; set; }

        [Display(Name = "عدد الاعمدة")]
        public int ArrayColmun { get; set; }
        [DataType(DataType.Text)]
        [Display(Name = "الوصف")]
        public string? Descreption { get; set; }

        [Display(Name = "الصورة")]
        public string? ImageUrl { get; set; }

        [DataType(DataType.Text)]
        public string? Objectstext { get; set; }
        public string[]? ObjectJson { get; set; }
        [Display(Name = "اسم المشروع")]

        [Required(ErrorMessage = "اسم المشروع ضروري")]
        public string? ProjectName { get; set; }
        public string? UserId { get; set; }
        public Guid TemplateId { get; set; }

        [Display(Name ="تاريخ انشاء المشروع")]
        [DataType(DataType.Date)]
        public DateTime CurrentDate { get; set; }


    }
}