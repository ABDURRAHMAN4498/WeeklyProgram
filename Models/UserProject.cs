using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeeklyProgram.Models
{
    public class UserProject
    {
        [Key]
        public Guid Id { get; set; }
        public string? UserId { get; set; }
        public string? TemplateId { get; set; }
        public Template? Template { get; set; }
        public string? ProjectTitle { get; set; }
        public string? Objectstext { get; set; }
        public string? ImageUrl { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
    }
}