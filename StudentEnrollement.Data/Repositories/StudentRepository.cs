using Microsoft.EntityFrameworkCore;
using StudentEnrollement.Data.Contracts;
using StudentEnrollement.Data.DatabaseContext;

namespace StudentEnrollement.Data.Repositories
{
    public class StudentRepository : GenericRepository<Stutent>, IStudentRepository
    {
        public StudentRepository(StudentEnrollementDbContext db) : base(db)
        {
        }

        public async Task<Stutent> GetStudentDetails(int studentId)
        {
            var student = await _db.Stutents
                .Include(q => q.Enrollements).ThenInclude(q => q.Course)
                .FirstOrDefaultAsync(p => p.Id == studentId);
            return student;
        }
    }
}
