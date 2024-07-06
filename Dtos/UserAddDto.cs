using System.ComponentModel.DataAnnotations;
using WeeklyProgram.Models;

namespace WeeklyProgram.Dtos
{
    public class UserAddDto
    {
        [Required(ErrorMessage =" الاسم ضروري")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage ="الكنية ضرورية")]
        public string? LastName { get; set; }

        [Required(ErrorMessage ="البريد الالكتروني ضروري")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage ="رقم الهاتف ضروري")]
        public string?  PhoneNumber { get; set; }

        [Required(ErrorMessage ="كلمة السر ضرورية")]
        [DataType(DataType.Password)]
        public string?  Password { get; set; }

        [Required(ErrorMessage ="هذا الحقل ضروري")]
        public Guid RoleId { get; set; }
        [Display(Name ="الصلاحيات")]
        public List<AppRole>? Roles { get; set; }
    }
}