﻿using FluentValidation;

namespace StudentEnrollement.Api.DTOs.Student
{
    public class CreateStudentDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdNumber { get; set; }
        public Byte[] ProfilePicture { get; set; }
        public string OriginalFileName { get; set; }
    }

    public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentDtoValidator() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();
            RuleFor(x => x.LastName)
                .NotEmpty();
            RuleFor(x => x.DateOfBirth)
                .NotEmpty();
            RuleFor(x => x.IdNumber)
                .NotEmpty();

            RuleFor(x => x.OriginalFileName)
                .NotNull()
                .When(x => x.ProfilePicture != null);
        }
    }
}
