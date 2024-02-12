using StudentEnrollement.Api.DTOs.Course;
using StudentEnrollement.Api.DTOs.Student;

namespace StudentEnrollement.Api.DTOs.Enrollement
{
    public class EnrollementDto
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        public virtual CourseDto Course { get; set; }
        public virtual StudentDto Student { get; set; }
    }
}
