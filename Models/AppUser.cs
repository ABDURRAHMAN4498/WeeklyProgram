using Microsoft.AspNetCore.Identity;

namespace WeeklyProgram.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Credit { get; set; }

    }
}