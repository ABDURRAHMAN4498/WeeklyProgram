using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using WeeklyProgram.Models;

namespace WeeklyProgram.Dtos
{
    public class UserUpdateDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = " الاسم ضروري")]
        [DataType(DataType.Text)]
        [Display(Name = "الاسم")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "الكنية ضرورية")]
        [DataType(DataType.Text)]
        [Display(Name = "الكنية")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "البريد الالكتروني ضروري")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "البريد الالكتروني")]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "رقم الهاتف ضروري")]
        [Display(Name = "رقم الهاتف")]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "كلمة السر ضرورية")]
        [Display(Name = "كلمة السر")]
        public string? Password { get; set; }

        public Guid RoleId { get; set; }
        [Display(Name = "الصلاحيات")]
        public List<AppRole>? Roles { get; set; }
    }
}