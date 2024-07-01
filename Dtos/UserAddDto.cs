using System.ComponentModel.DataAnnotations;
using WeeklyProgram.Models;

namespace WeeklyProgram.Dtos
{
    public class UserAddDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public string?  PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        public string?  Password { get; set; }
        public Guid RoleId { get; set; }
        public List<AppRole>? Roles { get; set; }
    }
}