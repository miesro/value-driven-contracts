using FluentValidation;
using LyfegenContracts.Api.Middleware;
using LyfegenContracts.Application.Contracts.Dtos;
using LyfegenContracts.Application.Contracts.Repositories;
using LyfegenContracts.Application.Contracts.Services;
using LyfegenContracts.Application.Contracts.Validation;
using LyfegenContracts.Application.Parties;
using LyfegenContracts.Application.Patients.Dtos;
using LyfegenContracts.Application.Patients.Repositories;
using LyfegenContracts.Application.Patients.Services;
using LyfegenContracts.Application.Patients.Validation;
using LyfegenContracts.Application.Products.Repositories;
using LyfegenContracts.Application.Treatments.Dtos;
using LyfegenContracts.Application.Treatments.Repositories;
using LyfegenContracts.Application.Treatments.Services;
using LyfegenContracts.Application.Treatments.Validation;
using LyfegenContracts.Infrastructure.Persistence;
using LyfegenContracts.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

const string ClientCors = "ClientCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(ClientCors, policy =>
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var conn = builder.Configuration.GetConnectionString("LyfegenDb");
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(conn));

builder.Services.AddScoped<IValidator<CreateContractDto>, CreateContractDtoValidator>();
builder.Services.AddScoped<IValidator<CreatePatientDto>, CreatePatientDtoValidator>();
builder.Services.AddScoped<IValidator<MatchContractsRequestDto>, MatchContractsRequestDtoValidator>();
builder.Services.AddScoped<IValidator<CreateTreatmentDto>, CreateTreatmentDtoValidator>();
builder.Services.AddScoped<IValidator<SetTreatmentOutcomeDto>, SetTreatmentOutcomeDtoValidator>();

builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<ITreatmentService, TreatmentService>();

builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPartyRepository, PartyRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<ITreatmentRepository, TreatmentRepository>();

builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseMiddleware<ProblemDetailsMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors(ClientCors);
app.UseAuthorization();

app.MapControllers();

app.Run();
