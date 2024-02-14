using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentEnrollement.Data.Configurations
{
     internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
        {
            public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
            {
                builder.HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = "c841b8b5-e2c0-4ed1-90ed-072a95fdbc25",
                        UserId = "408aa945-3d84-4421-8342-7269ec64d949",
                    },
                    new IdentityUserRole<string>
                    {
                        RoleId = "d34564b1-3686-4eef-8484-950c6c8a3de1",
                        UserId = "3f4631bd-f907-4409-b416-ba356312e659",
                    }
                );
            }
        }
    }
