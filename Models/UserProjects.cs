using System.ComponentModel.DataAnnotations.Schema;

namespace WeeklyProgram.Models
{
    public class UserProjects
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int TemplateId { get; set; }
        public Template? Template { get; set; }
        public string? ProjectTitle { get; set; }
        public string? Objectstext { get; set; }
        [NotMapped]
        public string[]? ObjectJson { get; set; }

    }
}