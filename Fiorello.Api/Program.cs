using AutoMapper;
using Fiorello.Core.Entities;
using Fiorello.Core.Repositories;
using Fiorello.Data;
using Fiorello.Data.Repositories;
using Fiorello.Services.Dtos.Category;
using Fiorello.Services.Dtos.Slider;
using Fiorello.Services.Implementations;
using Fiorello.Services.Interfaces;
using Fiorello.Services.Profiles;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);



//Validator Sevices start
builder.Services.AddFluentValidationRulesToSwagger();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryCreateDtoValidator>();
//Validator Services end

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//Mapper start
builder.Services.AddScoped(provider =>
new MapperConfiguration(opt =>
{
    opt.AddProfile(new MapProfile(provider.GetService<IHttpContextAccessor>()));
}
).CreateMapper());
//Mapper end

//SQL database start
builder.Services.AddDbContext<FiorelloDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric = false;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<FiorelloDbContext>();
//SQL database end

//Added Services start
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<ISliderRepository,SliderRepository>();
builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IFlowerImageRepository,FlowerImageRepository>();
builder.Services.AddScoped<IFlowerImageService,FlowerImageService>();
//Added Services end

builder.Services.AddHttpContextAccessor();

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
