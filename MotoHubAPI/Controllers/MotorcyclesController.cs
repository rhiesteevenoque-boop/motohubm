using System.Security.Claims; using Microsoft.AspNetCore.Authorization; using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore; using MotoHubAPI.Data; using MotoHubAPI.Models;
namespace MotoHubAPI.Controllers;
[ApiController,Authorize,Route("api/motorcycles")]
public class MotorcyclesController(MotoHubDbContext db):ControllerBase {
 int UserId=>int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
 [HttpGet] public async Task<IActionResult> Get()=>Ok(await db.Motorcycles.AsNoTracking().Where(x=>User.IsInRole("Admin")||x.UserId==UserId).ToListAsync());
 [HttpPost] public async Task<IActionResult> Create(Motorcycle x){x.Id=0;x.UserId=UserId;db.Motorcycles.Add(x);await db.SaveChangesAsync();return CreatedAtAction(nameof(Get),new{id=x.Id},x);}
 [HttpPut("{id:int}")] public async Task<IActionResult> Update(int id,Motorcycle input){var x=await db.Motorcycles.FirstOrDefaultAsync(x=>x.Id==id&&(User.IsInRole("Admin")||x.UserId==UserId));if(x is null)return NotFound();x.Make=input.Make;x.Model=input.Model;x.Year=input.Year;x.PlateNumber=input.PlateNumber;await db.SaveChangesAsync();return Ok(x);}
 [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id){var x=await db.Motorcycles.FirstOrDefaultAsync(x=>x.Id==id&&(User.IsInRole("Admin")||x.UserId==UserId));if(x is null)return NotFound();if(await db.Bookings.AnyAsync(b=>b.MotorcycleId==id))return Conflict(new{message="Cannot delete a motorcycle with booking history."});db.Motorcycles.Remove(x);await db.SaveChangesAsync();return NoContent();}
}
