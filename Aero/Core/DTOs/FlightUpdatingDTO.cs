using System.Text.Json.Serialization;

namespace Aero.Core.DTOs;

public class FlightUpdatingDTO
{
    [JsonConstructor]
    public FlightUpdatingDTO() { }
    
    public Guid Id { get; set; }
    public string FlightNumber { get; set; }
    public DateTime DepartureDate { get; set; }
    public Guid AirlineId { get; set; }
    
    public Dictionary<string, string> Validate()
    {
        var errors = new Dictionary<string, string>();

        if (Id == default)
            errors.Add(nameof(Id), "Id не указан");
        
        if (String.IsNullOrWhiteSpace(FlightNumber))
            errors.Add(nameof(FlightNumber), "Номер рейса не указан");
        else if (FlightNumber.Length > 10)
            errors.Add(nameof(FlightNumber), "Номер рейса не указан");
      
        if (DepartureDate == default)
            errors.Add(nameof(DepartureDate), "Указанная дата вылета не корректна");
        if (AirlineId == default)
            errors.Add(nameof(AirlineId), "Указанное Id авиакомпании не корректно");
      
        return errors;
    }
}