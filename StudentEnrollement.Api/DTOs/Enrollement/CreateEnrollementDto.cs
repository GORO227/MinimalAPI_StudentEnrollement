using FluentValidation;
using StudentEnrollement.Data.Contracts;

namespace StudentEnrollement.Api.DTOs.Enrollement
{
    public class CreateEnrollementDto
    {
        public int CourseId { get; set; }
        public int StutentId { get; set; }
    }

    public class CreateEnrollementDtoValidator : AbstractValidator<CreateEnrollementDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        public CreateEnrollementDtoValidator(ICourseRepository courseRepository, 
            IStudentRepository studentRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;

            RuleFor(x => x.CourseId)
                .MustAsync(async (id, token) =>
                {
                    var courseExits =  await _courseRepository.Exists(id);
                    return courseExits;
                }).WithMessage("{PropertyName} does not exist");

            RuleFor(x => x.StutentId)
               .MustAsync(async (id, token) =>
               {
                   var studentExits = await _studentRepository.Exists(id);
                   return studentExits;
               }).WithMessage("{PropertyName} does not exist");
        }
    }
}
