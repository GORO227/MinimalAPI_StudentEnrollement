using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using StudentEnrollement.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollement.Data.DatabaseContext
{
    public class StudentEnrollementDbContext : IdentityDbContext<SchoolUser>
    {
        public StudentEnrollementDbContext(DbContextOptions<StudentEnrollementDbContext> options)
            :base(options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Stutent> Stutents { get; set;}
        public DbSet<Enrollement> Enrollements { get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new SchoolUserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
        }
        
    }
    public class StudentEnrollementDbContextFactory : IDesignTimeDbContextFactory<StudentEnrollementDbContext>
    {
        public StudentEnrollementDbContext CreateDbContext(string[] args)
        {
            //Get environment
            //string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            //Build config
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            //Get connection string
            var optionsBuilder = new DbContextOptionsBuilder<StudentEnrollementDbContext>();
            var connectionString = config.GetConnectionString("StudentEnrollementDbConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return new StudentEnrollementDbContext(optionsBuilder.Options);
        }
    }
}
