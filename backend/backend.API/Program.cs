using backend.Application;
using backend.Application.Interfaces;
using backend.Application.Interfaces.Persistence;
using backend.Application.Models;
using backend.Application.Services;
using backend.Infrastructure;
using backend.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddApplication().AddInfrastructure();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapPost("/api/appointment/Post", async (IAppointmentService appointmentService, AppointmentInDTO appointmentInDTO) =>
{
    return Results.Ok(await appointmentService.CreateAppointmentAsync(appointmentInDTO));
});

app.MapPut("/api/appointment/{id}", async (int id, IAppointmentService appointmentService, AppointmentInDTO appointmentInDTO) =>
{
    var result = await appointmentService.UpdateAppointmentAsync(id, appointmentInDTO);
    return result.Item1 ? Results.Ok(result.Item2) : Results.NotFound(result.Item2);
});

app.MapDelete("/api/appointment/{id}", async (int id, IAppointmentService appointmentService) =>
{
    var result = await appointmentService.DeleteAppointmentAsync(id);
    return result.Item1 ? Results.Ok(result.Item2) : Results.NotFound(result.Item2);
});

app.MapGet("/api/appointment/{id}", async (int id, IAppointmentService appointmentService) =>
{
    var result = await appointmentService.GetByIdAppointmentAsync(id);
    return Results.Ok(result.Item1);
});

app.MapPost("/api/appointment/getall", async (IAppointmentService appointmentService, AppointmentFilterInDTO appointmentFilterInDTO) =>
{
    var result = await appointmentService.GetAllAppointmentAsync(appointmentFilterInDTO);
    return Results.Ok(result.Item1);
});

app.MapGet("/api/patients/getdropdownlist", async (IDropdownListRepository dropdownListRepository) =>
{
    return Results.Ok(await dropdownListRepository.PatientDropdownList());
});

app.MapGet("/api/doctors/getdropdownlist", async (IDropdownListRepository dropdownListRepository) =>
{
    return Results.Ok(await dropdownListRepository.DoctorDropdownList());
});

app.MapGet("/api/prescription/download/{id}", async (int id, PrescriptionPdfService prescriptionPdfService, IAppointmentService appointmentService) =>
{
    var appointment = await appointmentService.GetByIdAppointmentAsync(id);
    return Results.File(await Task.Run(() => prescriptionPdfService.GeneratePrescriptionPdf(appointment.Item1)), "application/pdf", "masumbillah");
});

app.Run();
