using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollement.Data;
using StudentEnrollement.Data.DatabaseContext;
using AutoMapper;
using StudentEnrollement.Api.DTOs.Enrollement;
using StudentEnrollement.Api.DTOs.Student;
using StudentEnrollement.Data.Contracts;
using Microsoft.AspNetCore.Authorization;
namespace StudentEnrollement.Api.Endpoints;

public static class StutentEndpoints
{
    public static void MapStutentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Stutent").WithTags(nameof(Stutent));

        group.MapGet("/", async (IStudentRepository repo,IMapper mapper) =>
        {
            var students = await repo.GetAllAsync();
            var data = mapper.Map<List<StudentDto>>(students);
            return data;
        })
        .AllowAnonymous()
        .WithName("GetAllStutents")
        .WithOpenApi()
        .Produces<List<StudentDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, IStudentRepository repo,IMapper mapper) =>
        {
            return await repo.GetAsync(id)
                is Stutent model
                    ? Results.Ok(mapper.Map<StudentDto>(model))
                    : Results.NotFound();
        })
        .WithName("GetStutentById")
        .WithOpenApi()
        .Produces<Stutent>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/GetCourses/{id}", async (int id, IStudentRepository repo, IMapper mapper) =>
        {
            return await repo.GetStudentDetails(id)
                is Stutent model
                    ? Results.Ok(mapper.Map<StutentDetailsDto>(model))
                    : Results.NotFound();
        })
        .WithName("GetStutentDetailsById")
        .WithOpenApi()
        .Produces<StutentDetailsDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", [Authorize(Roles = "Administrator")] async (int id, StudentDto stutentDto, IStudentRepository repo, IMapper mapper) =>
        {
            var foundModel = await repo.GetAsync(id);
            if (foundModel is null)
                return Results.NotFound();

            mapper.Map(stutentDto, foundModel);
            await repo.UpdateAsync(foundModel);
            return Results.NoContent();

            //var affected = await db.Stutents
            //    .Where(model => model.Id == id)
            //    .ExecuteUpdateAsync(setters => setters
            //        .SetProperty(m => m.FirstName, stutent.FirstName)
            //        .SetProperty(m => m.LastName, stutent.LastName)
            //        .SetProperty(m => m.DateOfBirth, stutent.DateOfBirth)
            //        .SetProperty(m => m.IdNumber, stutent.IdNumber)
            //        .SetProperty(m => m.Picture, stutent.Picture)
            //        .SetProperty(m => m.Id, stutent.Id)
            //        .SetProperty(m => m.CreatedDate, stutent.CreatedDate)
            //        .SetProperty(m => m.CreatedBy, stutent.CreatedBy)
            //        .SetProperty(m => m.ModifiedDate, stutent.ModifiedDate)
            //        .SetProperty(m => m.ModifiedBy, stutent.ModifiedBy)
            //        );
            //return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("UpdateStutent")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", [Authorize(Roles = "Administrator")] async (CreateStudentDto stutentDto, IStudentRepository repo, IMapper mapper) =>
        {
            var stutent = mapper.Map<Stutent>(stutentDto);
            await repo.AddAsync(stutent);
            return Results.Created($"/api/Stutent/{stutent.Id}", stutent);
        })
        .WithName("CreateStutent")
        .WithOpenApi()
        .Produces<Stutent>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", [Authorize(Roles = "Administrator")] async (int id, IStudentRepository repo) =>
        {
            return await repo.DeleteAsync(id) ? Results.NoContent() : Results.NotFound();
            //var affected = await db.Stutents
            //    .Where(model => model.Id == id)
            //    .ExecuteDeleteAsync();
            //return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteStutent")
        .WithOpenApi()
        .Produces<Stutent>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
