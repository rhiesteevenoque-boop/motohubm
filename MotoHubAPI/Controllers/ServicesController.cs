using Microsoft.AspNetCore.Authorization; using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore; using MotoHubAPI.Data; using MotoHubAPI.Models;
namespace MotoHubAPI.Controllers;
[ApiController,Route("api/services")]
public class ServicesController(MotoHubDbContext db):ControllerBase {
 [HttpGet] public async Task<IActionResult> Get()=>Ok(await db.Services.AsNoTracking().Where(x=>x.IsActive).ToListAsync());
 [HttpGet("{id:int}")] public async Task<IActionResult> GetOne(int id){var x=await db.Services.FindAsync(id);return x is null?NotFound():Ok(x);}
 [Authorize(Roles="Admin"),HttpPost] public async Task<IActionResult> Create(ServiceItem x){x.Id=0;db.Services.Add(x);await db.SaveChangesAsync();return CreatedAtAction(nameof(GetOne),new{id=x.Id},x);}
 [Authorize(Roles="Admin"),HttpPut("{id:int}")] public async Task<IActionResult> Update(int id,ServiceItem input){var x=await db.Services.FindAsync(id);if(x is null)return NotFound();x.Name=input.Name;x.Description=input.Description;x.Price=input.Price;x.DurationMinutes=input.DurationMinutes;x.IsActive=input.IsActive;await db.SaveChangesAsync();return Ok(x);}
 [Authorize(Roles="Admin"),HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id){var x=await db.Services.FindAsync(id);if(x is null)return NotFound();x.IsActive=false;await db.SaveChangesAsync();return NoContent();}
}
