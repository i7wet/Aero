import Airline from "@/Models/Airline.js";

export default class Flight{
    constructor(id, flightNumber, departureDate, airline) {
        this.Id = id;
        this.FlightNumber = flightNumber;
        this.DepartureDate = departureDate;
        this.Airline = airline
    }
    Id;
    FlightNumber;
    DepartureDate;
    Airline;
}