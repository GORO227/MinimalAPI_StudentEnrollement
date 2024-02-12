namespace StudentEnrollement.Data.Contracts
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<Course> GetStudentList(int courseId);
    }
}
