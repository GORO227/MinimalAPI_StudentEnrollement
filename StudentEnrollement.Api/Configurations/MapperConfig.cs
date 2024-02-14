using AutoMapper;
using StudentEnrollement.Api.DTOs.Authentication;
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
            CreateMap<Course, CourseDetailsDto>()
                .ForMember(q => q.Stutents, x => x.MapFrom(course => course.Enrollements.Select(stu => stu.Stutent)));

            CreateMap<Stutent, StudentDto>().ReverseMap();
            CreateMap<Stutent, CreateStudentDto>().ReverseMap();
            CreateMap<Stutent, StutentDetailsDto>()
                .ForMember(q => q.Courses, x => x.MapFrom(stutent => stutent.Enrollements.Select(cr => cr.Course)));

            CreateMap<Enrollement, EnrollementDto>().ReverseMap();
            CreateMap<Enrollement, CreateEnrollementDto>().ReverseMap();

            CreateMap<RegisterDto, SchoolUser>();
        }
    }
}
