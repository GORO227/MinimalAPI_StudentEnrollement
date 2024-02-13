using StudentEnrollement.Api.DTOs.Course;
using StudentEnrollement.Api.DTOs.Enrollement;

namespace StudentEnrollement.Api.DTOs.Student
{
    public class StutentDetailsDto : CreateStudentDto
    {
        public List<CourseDto> Courses { get; set; } = new();
    }
}
