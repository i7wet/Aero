using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aero.Database.Models;

[Index(nameof(FlightNumber), IsUnique = true)]
public class FlightDb
{
    [Key]
    public Guid Id { get; set; }
    public string FlightNumber { get; set; }
    public DateTime DepartureDate { get; set; }
    public AirlineDb Airline { get; set; }
}