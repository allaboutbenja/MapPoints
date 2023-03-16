using Microsoft.EntityFrameworkCore;
using testAPI.Data;
using testAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PostgresSQLConnection");
builder.Services.AddDbContext<OfficeDb>(options =>
    options.UseNpgsql(connectionString));


var app = builder.Build();

app.UseCors(options =>
{
    options.WithOrigins("http://127.0.0.1:5500");
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/points/", async (MapPoint e, OfficeDb db) =>
{
    db.MapPoints.Add(e);
    await db.SaveChangesAsync();
    //Metodo que utiliza Entity Framework para guardar todos los cambios aplicados.

    return Results.Created($"/points/{e.Id}", e);
    //Permite crear las respuestas.
});

app.MapGet("/points/{id:int}", async (int id, OfficeDb db) =>
{
    return await db.MapPoints.FindAsync(id)
        is MapPoint e
        ? Results.Ok(e)
        : Results.NotFound();

});

app.MapGet("/points/", async (OfficeDb db) => await db.MapPoints.ToListAsync());
// GetAll. 

app.MapPut("/points/{id:int}", async (int id, MapPoint e, OfficeDb db) =>
{
    if (e.Id != id)
        return Results.BadRequest();

    var mapPoint = await db.MapPoints.FindAsync(id);

    if (mapPoint is null) return Results.NotFound();

    mapPoint.Rut = e.Rut;
    mapPoint.FirstName = e.FirstName;
    mapPoint.LastName = e.LastName;
    mapPoint.Branch = e.Branch;
    mapPoint.PhoneNumber = e.PhoneNumber;
    mapPoint.Latitude = e.Latitude;
    mapPoint.Longitude= e.Longitude;

    await db.SaveChangesAsync();

    return Results.Ok(mapPoint);
});

app.MapDelete("/points/{id:int}", async (int id, OfficeDb db) =>
{
    var mapPoint = await db.MapPoints.FindAsync(id);

    if (mapPoint is null) return Results.NotFound();

    db.MapPoints.Remove(mapPoint);
    await db.SaveChangesAsync();

    return Results.NoContent();

});


app.Run();


