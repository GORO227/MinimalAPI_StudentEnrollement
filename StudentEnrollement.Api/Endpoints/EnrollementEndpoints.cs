using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollement.Data;
using StudentEnrollement.Data.DatabaseContext;
using AutoMapper;
using StudentEnrollement.Api.DTOs.Enrollement;
using StudentEnrollement.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
using StudentEnrollement.Api.DTOs.Authentication;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
namespace StudentEnrollement.Api.Endpoints;

public static class EnrollementEndpoints
{
    public static void MapEnrollementEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Enrollement").WithTags(nameof(Enrollement));

        group.MapGet("/", async (IEnrollmentRepository repo, IMapper mapper) =>
        {
            var enrollments = await repo.GetAllAsync();
            var data = mapper.Map<List<Enrollement>>(enrollments);
            return data;
        })
        .WithName("GetAllEnrollements")
        .WithOpenApi()
        .Produces<List<EnrollementDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, IEnrollmentRepository repo,IMapper mapper) =>
        {
            return await repo.GetAsync(id)
                is Enrollement model
                    ? Results.Ok(mapper.Map<EnrollementDto>(model))
                    : Results.NotFound();
        })
        .WithName("GetEnrollementById")
        .WithOpenApi()
        .Produces<EnrollementDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async (int id, EnrollementDto enrollementDto, IEnrollmentRepository repo, IMapper mapper, IValidator<EnrollementDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(enrollementDto);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.ToDictionary());
            }
            //var affected = await db.Enrollements
            //    .Where(model => model.Id == id)
            //    .ExecuteUpdateAsync(setters => setters
            //        .SetProperty(m => m.CourseId, enrollementDto.CourseId)
            //        .SetProperty(m => m.StudentId, enrollementDto.StudentId)
            //        );
            //return affected == 1 ? Results.Ok() : Results.NotFound();

            var foundModel = await repo.GetAsync(id);
            if(foundModel is null)
                return Results.NotFound();

            mapper.Map(enrollementDto, foundModel);
            await repo.UpdateAsync(foundModel);
            return Results.NoContent();
        })
        .WithName("UpdateEnrollement")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (CreateEnrollementDto enrollementDto, IEnrollmentRepository repo,IMapper mapper, IValidator<CreateEnrollementDto> validator) =>
        {
            var validationResult = await validator.ValidateAsync(enrollementDto);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.ToDictionary());
            }
            var enrollement = mapper.Map<Enrollement>(enrollementDto);
            await repo.AddAsync(enrollement);
            return Results.Created($"/api/Enrollement/{enrollement.Id}", enrollement);
        })
        .WithName("CreateEnrollement")
        .WithOpenApi()
        .Produces<Enrollement>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", [Authorize(Roles = "Administrator")] async (int id, IEnrollmentRepository repo) =>
        {
            return await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteEnrollement")
        .WithOpenApi()
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
