using _2GuysHandyman.ApiModelsValidators;
using _2GuysHandyman.models;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
       .AddFluentValidation(options => {
          options.ImplicitlyValidateChildProperties = true;
          options.ImplicitlyValidateRootCollectionElements = true;
          options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
       });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<dbContext>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
