using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollement.Data;
using StudentEnrollement.Data.DatabaseContext;
using StudentEnrollement.Api.DTOs.Course;
using AutoMapper;
using StudentEnrollement.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using FluentValidation;
using StudentEnrollement.Api.DTOs.Authentication;
using System.ComponentModel.DataAnnotations;
namespace StudentEnrollement.Api.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Course").WithTags(nameof(Course));
        //Without Autorization tag AllowAnonymous() or  [AllowAnonymous] 

        group.MapGet("/", [AllowAnonymous] async (ICourseRepository repo, IMapper mapper) =>
        {
            var courses = await repo.GetAllAsync();
            var data = mapper.Map<List<CourseDto>>(courses);
            return data;
        })
        .WithName("GetAllCourses")
        .WithOpenApi()
        .Produces<List<CourseDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, ICourseRepository repo, IMapper mapper) =>
        {
            return await repo.GetAsync(id)
                is Course model
                    ? Results.Ok(mapper.Map<CourseDto>(model))
                    : Results.NotFound();
        })
        .AllowAnonymous()
        .WithName("GetCourseById")
        .WithOpenApi()
        .Produces<CourseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/GetStudents/{id}", async (int id, ICourseRepository repo, IMapper mapper) =>
        {
            return await repo.GetStudentList(id)
                is Course model
                    ? Results.Ok(mapper.Map<CourseDetailsDto>(model))
                    : Results.NotFound();
        })
       .WithName("GetCourseDetailsById")
       .WithOpenApi()
       .Produces<CourseDetailsDto>(StatusCodes.Status200OK)
       .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async (int id, CourseDto courseDto, ICourseRepository repo, IMapper mapper, IValidator<CourseDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(courseDto);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.ToDictionary());
            }

            var foundModel = await repo.GetAsync(id);
            if (foundModel is null)
                return Results.NotFound();

            mapper.Map(courseDto, foundModel);
            await repo.UpdateAsync(foundModel);
            return Results.NoContent();

            //var affected = await db.Courses
            //    .Where(model => model.Id == id)
            //    .ExecuteUpdateAsync(setters => setters
            //        .SetProperty(m => m.Title, course.Title)
            //        .SetProperty(m => m.Credits, course.Credits)
            //        .SetProperty(m => m.Id, course.Id)
            //        //.SetProperty(m => m.CreatedDate, course.CreatedDate)
            //        //.SetProperty(m => m.CreatedBy, course.CreatedBy)
            //        //.SetProperty(m => m.ModifiedDate, course.ModifiedDate)
            //        //.SetProperty(m => m.ModifiedBy, course.ModifiedBy)
            //        );
            // return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("UpdateCourse")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (CreateCourseDto courseDto, ICourseRepository repo, IMapper mapper, IValidator<CreateCourseDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(courseDto);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.ToDictionary());
            }
            var course = mapper.Map<Course>(courseDto);
            await repo.AddAsync(course);
            return Results.Created($"/api/Course/{course.Id}", course);
        })
        .WithName("CreateCourse")
        .WithOpenApi()
        .Produces<Course>(StatusCodes.Status201Created);
        //mitple Roles => [Authorize(Roles = "Administrator, User")]
        group.MapDelete("/{id}", [Authorize(Roles = "Administrator")] async  (int id, ICourseRepository repo,IMapper mapper) =>
        {
            return await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteCourse")
        .WithOpenApi()
        .Produces<Course>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
