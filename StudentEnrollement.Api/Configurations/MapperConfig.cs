using AutoMapper;
using StudentEnrollement.Api.DTOs.Course;
using StudentEnrollement.Api.DTOs.Enrollement;
using StudentEnrollement.Api.DTOs.Student;
using StudentEnrollement.Data;

namespace StudentEnrollement.Api.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CreateCourseDto>().ReverseMap();

            CreateMap<Stutent, StudentDto>().ReverseMap();
            CreateMap<Stutent, CreateStudentDto>().ReverseMap();

            CreateMap<Enrollement, EnrollementDto>().ReverseMap();
            CreateMap<Enrollement, CreateEnrollementDto>().ReverseMap();
        }
    }
}
