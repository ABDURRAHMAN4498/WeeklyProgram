using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace WeeklyProgram.Dtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage ="هذا الحقل ضروري")]
        [Display(Name ="البريد الالكتروني")]
        public string? Email { get; set; }
        [Display(Name ="كلمة السر")]
        [Required(ErrorMessage ="هذا الحقل ضروري")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public  bool RememberMe { get; set; }
    }
}