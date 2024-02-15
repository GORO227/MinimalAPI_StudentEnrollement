using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using StudentEnrollement.Api.DTOs;
using StudentEnrollement.Api.DTOs.Authentication;
using StudentEnrollement.Api.DTOs.Course;
using StudentEnrollement.Api.Filters;
using StudentEnrollement.Api.Services;
using StudentEnrollement.Data;
using StudentEnrollement.Data.Contracts;
using System.ComponentModel.DataAnnotations;

namespace StudentEnrollement.Api.Endpoints
{
    public static partial class AuthenticationEndpoints
    {
        public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/Auth").WithTags("Authentication");

            group.MapPost("/login/", async (LoginDto loginDto, IAuthManager authManager) =>
            {
                var response = await authManager.Login(loginDto);
                if(response is null)
                {
                    return Results.Unauthorized();
                }
                return Results.Ok(response);       
            })
            .AddEndpointFilter<ValidationFilter<LoginDto>>()
            .AllowAnonymous()
            .WithName("Login")
            .WithOpenApi()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

            group.MapPost("/register/", async (RegisterDto registerDto, IAuthManager authManager) =>
            {
                var response = await authManager.Register(registerDto);
                if(!response.Any())
                    return Results.Ok();

                var errors = new List<ErrorResponseDto>();

                foreach (var error in response)
                    errors.Add(new ErrorResponseDto
                    {
                        Code = error.Code,
                        Description = error.Description
                    });
   
                return Results.BadRequest(errors);
            })
            .AddEndpointFilter<ValidationFilter<RegisterDto>>()
            .AllowAnonymous()
            .WithName("Register")
            .WithOpenApi()
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);
        }
    }
}
