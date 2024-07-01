using System.ComponentModel.DataAnnotations;
using WeeklyProgram.Models;

namespace WeeklyProgram.Dtos
{
    public class UserAddDto
    {
        [Required(ErrorMessage ="هذا الحقل ضروري")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage ="هذا الحقل ضروري")]
        public string? LastName { get; set; }

        [Required(ErrorMessage ="هذا الحقل ضروري")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage ="هذا الحقل ضروري")]
        public string?  PhoneNumber { get; set; }

        [Required(ErrorMessage ="هذا الحقل ضروري")]
        [DataType(DataType.Password)]
        public string?  Password { get; set; }

        [Required(ErrorMessage ="هذا الحقل ضروري")]
        public Guid RoleId { get; set; }

        [Required(ErrorMessage ="هذا الحقل ضروري")]
        public List<AppRole>? Roles { get; set; }
    }
}