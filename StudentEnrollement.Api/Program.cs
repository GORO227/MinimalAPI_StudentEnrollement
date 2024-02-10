using Microsoft.EntityFrameworkCore;
using StudentEnrollement.Data.DatabaseContext;

var builder = WebApplication.CreateBuilder(args);

var con = builder.Configuration.GetConnectionString("StudentEnrollementDbConnection");
builder.Services.AddDbContext<StudentEnrollementDbContext>(options =>
{
    options.UseSqlServer(con);
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy 
        => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");


app.Run();
