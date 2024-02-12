using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollement.Data;
using StudentEnrollement.Data.DatabaseContext;
using AutoMapper;
using StudentEnrollement.Api.DTOs.Enrollement;
namespace StudentEnrollement.Api.Endpoints;

public static class EnrollementEndpoints
{
    public static void MapEnrollementEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Enrollement").WithTags(nameof(Enrollement));

        group.MapGet("/", async (StudentEnrollementDbContext db, IMapper mapper) =>
        {
            var enrollments = await db.Enrollements.ToListAsync();
            var data = mapper.Map<List<Enrollement>>(enrollments);
            return data;
        })
        .WithName("GetAllEnrollements")
        .WithOpenApi()
        .Produces<List<EnrollementDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, StudentEnrollementDbContext db,IMapper mapper) =>
        {
            return await db.Enrollements.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Enrollement model
                    ? Results.Ok(mapper.Map<EnrollementDto>(model))
                    : Results.NotFound();
        })
        .WithName("GetEnrollementById")
        .WithOpenApi()
        .Produces<EnrollementDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async (int id, EnrollementDto enrollementDto, StudentEnrollementDbContext db, IMapper mapper) =>
        {
            //var affected = await db.Enrollements
            //    .Where(model => model.Id == id)
            //    .ExecuteUpdateAsync(setters => setters
            //        .SetProperty(m => m.CourseId, enrollementDto.CourseId)
            //        .SetProperty(m => m.StudentId, enrollementDto.StudentId)
            //        );
            //return affected == 1 ? Results.Ok() : Results.NotFound();

            var foundModel = await db.Enrollements.FindAsync(id);
            if(foundModel is null)
                return Results.NotFound();

            mapper.Map(enrollementDto, foundModel);
            await db.SaveChangesAsync();
            return Results.NoContent();
        })
        .WithName("UpdateEnrollement")
        .WithOpenApi()
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async (CreateEnrollementDto enrollementDto, StudentEnrollementDbContext db,IMapper mapper) =>
        {
            var enrollement = mapper.Map<Enrollement>(enrollementDto);
            db.Enrollements.Add(enrollement);
            await db.SaveChangesAsync();
            return Results.Created($"/api/Enrollement/{enrollement.Id}", enrollement);
        })
        .WithName("CreateEnrollement")
        .WithOpenApi()
        .Produces<Enrollement>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async (int id, StudentEnrollementDbContext db) =>
        {
            var affected = await db.Enrollements
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteEnrollement")
        .WithOpenApi()
        .Produces<Enrollement>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
