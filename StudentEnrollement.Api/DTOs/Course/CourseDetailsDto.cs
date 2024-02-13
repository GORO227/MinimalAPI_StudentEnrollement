using StudentEnrollement.Api.DTOs.Student;

namespace StudentEnrollement.Api.DTOs.Course
{
    public class CourseDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public List<StudentDto> Stutents { get; set; } = new();
    }
}
