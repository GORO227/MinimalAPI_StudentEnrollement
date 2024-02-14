using FluentValidation;
using StudentEnrollement.Api.DTOs.Course;
using StudentEnrollement.Api.DTOs.Student;
using StudentEnrollement.Data.Contracts;

namespace StudentEnrollement.Api.DTOs.Enrollement
{
    public class EnrollementDto : CreateEnrollementDto
    {
        public virtual CourseDto Course { get; set; }
        public virtual StudentDto Stutent { get; set; }
    }

    public class EnrollementDtoValidator : AbstractValidator<EnrollementDto>
    {
        public EnrollementDtoValidator(ICourseRepository courseRepository,
            IStudentRepository studentRepository) 
        {
            Include(new CreateEnrollementDtoValidator(courseRepository, studentRepository));
        }
    }

}
