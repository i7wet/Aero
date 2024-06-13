using System.Diagnostics;
using Aero.Core.DTOs;
using Aero.Database;
using Aero.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aero.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController : ControllerBase
{
    private readonly ILogger<FlightsController> _logger;
    private readonly AeroDbContext _aeroDbContext;

    public FlightsController(ILogger<FlightsController> logger, AeroDbContext aeroDbContext)
    {
        _logger = logger;
        _aeroDbContext = aeroDbContext;
    }
    
    [HttpGet]
    public async Task<IResult> Get()
    {
        var flightDb = await _aeroDbContext.Flights.Select(x => x).Include(flight => flight.Airline).OrderBy(x => x.DepartureDate).ToListAsync();
        return Results.Json(flightDb);
    }

    [HttpPost]
    public async Task<IResult> Post([FromForm] FlightCreatingDTO flightCreatingDTO)
    {
        var errors = flightCreatingDTO.Validate();
        if (errors.Count != 0)
            return Results.BadRequest("Не верные данные при создании рейса");
        
        var airlineDbs = await _aeroDbContext.Airlines.Where(x => x.Id == flightCreatingDTO.AirlineId).ToListAsync();
        if (airlineDbs.Count == 0)
            return Results.BadRequest("Не удалось получить авиакомпанию");
        
        var airlineDb = airlineDbs.FirstOrDefault();

        var flightDb = new FlightDb()
        {
            Id = Guid.NewGuid(),
            DepartureDate = flightCreatingDTO.DepartureDate,
            Airline = airlineDb,
            FlightNumber = flightCreatingDTO.FlightNumber
        };

        _aeroDbContext.Flights.Add(flightDb);
        await _aeroDbContext.SaveChangesAsync();
        return Results.Ok();
    }

    [HttpPut]
    public async Task<IResult> Put([FromForm] FlightUpdatingDTO flightUpdatingDTO)
    {
        var errors = flightUpdatingDTO.Validate();
        if (errors.Count != 0)
            return Results.BadRequest("Не верные данные при обновлении рейса");
        
        var flightsDbs = await _aeroDbContext.Flights.Where(x => x.Id == flightUpdatingDTO.Id).ToListAsync();
        if (flightsDbs.Count == 0)
            return Results.BadRequest("Не удалось получить авиакомпанию");
        var flightDb = flightsDbs.FirstOrDefault();
        
        var airlineDbs = await _aeroDbContext.Airlines.Where(x => x.Id == flightUpdatingDTO.AirlineId).ToListAsync();
        if (airlineDbs.Count == 0)
            return Results.BadRequest("Не удалось получить авиакомпанию");
        var airlineDb = airlineDbs.FirstOrDefault();
        
        flightDb.FlightNumber = flightUpdatingDTO.FlightNumber;
        flightDb.DepartureDate = flightUpdatingDTO.DepartureDate;
        flightDb.Airline = airlineDb;
        
        _aeroDbContext.Flights.Update(flightDb);
        await _aeroDbContext.SaveChangesAsync();
        return Results.Ok();

    }

}