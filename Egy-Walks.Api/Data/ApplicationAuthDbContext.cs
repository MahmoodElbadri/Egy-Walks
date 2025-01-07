using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Egy_Walks.Api.Data;

public class ApplicationAuthDbContext : IdentityDbContext
{
    public ApplicationAuthDbContext(DbContextOptions<ApplicationAuthDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        string adminRoleId = "b44812d7-012d-4484-bb7f-bbd772c3d7d4";
        string userRoleId = "9d47aca6-a1de-4d8d-8a5d-349791564ead";

        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new IdentityRole
            {
                Id = userRoleId,
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
        };

        builder.Entity<IdentityRole>().HasData(roles);

    }
}