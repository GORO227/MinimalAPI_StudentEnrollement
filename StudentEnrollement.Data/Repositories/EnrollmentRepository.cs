using StudentEnrollement.Data.Contracts;
using StudentEnrollement.Data.DatabaseContext;

namespace StudentEnrollement.Data.Repositories
{
    public class EnrollmentRepository : GenericRepository<Enrollement>, IEnrollmentRepository
    {
        public EnrollmentRepository(StudentEnrollementDbContext db) : base(db)
        {
        }
    }
}
