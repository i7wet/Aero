using Aero.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Aero.Controllers;
[ApiController]
[Route("[controller]")]
public class AirlinesController : ControllerBase
{
    private readonly ILogger<AirlinesController> _logger;
    private readonly AeroDbContext _aeroDbContext;

    public AirlinesController(ILogger<AirlinesController> logger, AeroDbContext aeroDbContext)
    {
        _logger = logger;
        _aeroDbContext = aeroDbContext;
    }
    [HttpGet]
    public async Task<IResult> Get()
    {
        var airlineDb = await _aeroDbContext.Airlines.Select(x => x).ToListAsync();
        return Results.Json(airlineDb);
    }
}