using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeeklyProgram.Models
{
    public class UserProject
    {
        [Key]
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("Template")]
        public Guid? TemplateId { get; set; }
        
        public Template? Template { get; set; }
        [Display(Name ="اسم المشروع")]
        public string? ProjectTitle { get; set; }
        public string? Objectstext { get; set; }
        [Display(Name ="الصورة")]
        public string? ImageUrl { get; set; }
        [Display(Name ="تاريخ الانشاء")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
    }
}