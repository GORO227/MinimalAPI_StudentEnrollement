using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentEnrollement.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentEnrollement.Data.DatabaseContext
{
    public class StudentEnrollementDbContext : IdentityDbContext
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
            builder.ApplyConfiguration(new UserRoleConfiguration());
            
        }
    }
}
