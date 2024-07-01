using System.ComponentModel.DataAnnotations;

namespace WeeklyProgram.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public  bool EmailConfirmed { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        public int AccessFaildCount { get; set; }
        public string? Role { get; set; }

    }
}