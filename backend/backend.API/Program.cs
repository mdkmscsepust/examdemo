using backend.Application;
using backend.Application.Interfaces;
using backend.Application.Models;
using backend.Infrastructure;
using backend.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddApplication().AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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

app.MapPost("/api/appointment", async (IAppointmentService appointmentService, AppointmentFilterInDTO appointmentFilterInDTO) =>
{
    var result = await appointmentService.GetAllAppointmentAsync(appointmentFilterInDTO);
    return Results.Ok(result.Item1);
});
app.Run();
