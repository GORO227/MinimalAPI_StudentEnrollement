using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using StudentEnrollement.Data;
using StudentEnrollement.Data.DatabaseContext;
using AutoMapper;
using StudentEnrollement.Api.DTOs.Enrollement;
using StudentEnrollement.Api.DTOs.Student;
namespace StudentEnrollement.Api.Endpoints;

public static class StutentEndpoints
{
    public static void MapStutentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Stutent").WithTags(nameof(Stutent));

        group.MapGet("/", async (StudentEnrollementDbContext db,IMapper mapper) =>
        {
            var students = await db.Stutents.ToListAsync();
            var data = mapper.Map<List<StudentDto>>(students);
            return data;
        })
        .WithName("GetAllStutents")
        .WithOpenApi()
        .Produces<List<StudentDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async (int id, StudentEnrollementDbContext db,IMapper mapper) =>
        {
            return await db.Stutents.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Stutent model
                    ? Results.Ok(mapper.Map<StudentDto>(model))
                    : Results.NotFound();
        })
        .WithName("GetStutentById")
        .WithOpenApi()
        .Produces<Stutent>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async (int id, StudentDto stutentDto, StudentEnrollementDbContext db, IMapper mapper) =>
        {
            var foundModel = await db.Stutents.FindAsync(id);
            if (foundModel is null)
                return Results.NotFound();

            mapper.Map(stutentDto, foundModel);
            await db.SaveChangesAsync();
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

        group.MapPost("/", async (CreateStudentDto stutentDto, StudentEnrollementDbContext db, IMapper mapper) =>
        {
            var stutent = mapper.Map<Stutent>(stutentDto);
            db.Stutents.Add(stutent);
            await db.SaveChangesAsync();
            return Results.Created($"/api/Stutent/{stutent.Id}", stutent);
        })
        .WithName("CreateStutent")
        .WithOpenApi()
        .Produces<Stutent>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async (int id, StudentEnrollementDbContext db) =>
        {
            var affected = await db.Stutents
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? Results.Ok() : Results.NotFound();
        })
        .WithName("DeleteStutent")
        .WithOpenApi()
        .Produces<Stutent>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
