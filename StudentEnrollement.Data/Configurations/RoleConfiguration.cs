using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentEnrollement.Data.Configurations
{
    internal partial class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
               new IdentityRole
               {
                   Id = "c841b8b5-e2c0-4ed1-90ed-072a95fdbc25",
                   Name = "Administrator",
                   NormalizedName = "ADMINISTRATOR"
               },
               new IdentityRole
               {
                   Id = "d34564b1-3686-4eef-8484-950c6c8a3de1",
                   Name = "User",
                   NormalizedName ="USER"
               }
           );
        }
    }
}
