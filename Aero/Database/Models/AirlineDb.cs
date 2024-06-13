using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Aero.Database.Models;

[Index(nameof(FullName), nameof(IATACode), IsUnique = true)]
public class AirlineDb
{
    [Key]
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string IATACode { get; set; }
   
}