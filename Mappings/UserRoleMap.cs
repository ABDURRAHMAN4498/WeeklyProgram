using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeeklyProgram.Models;

namespace WeeklyProgram.Mappings
{
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            builder.HasData(
                new AppUserRole
                {
                    UserId = Guid.Parse("9AD102D5-5EC1-4207-9815-FC16A0634644"),
                    RoleId = Guid.Parse("7D26BE35-9699-437A-8C1F-548E26CA0DF0")
                },
                new AppUserRole
                {
                    UserId = Guid.Parse("5D91F752-F7FD-45BD-A099-4394DA465AB6"),
                    RoleId = Guid.Parse("A984B9BC-20DB-4AD0-AC39-EE9C126807B1")
                }
            );
        }
    }
}