using Microsoft.AspNetCore.Authorization; using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore; using MotoHubAPI.Data; using MotoHubAPI.Models;
namespace MotoHubAPI.Controllers;
[ApiController,Route("api/parts")]
public class PartsController(MotoHubDbContext db):ControllerBase {
 [HttpGet] public async Task<IActionResult> Get()=>Ok(await db.Parts.AsNoTracking().Where(x=>x.IsActive).ToListAsync());
 [HttpGet("{id:int}")] public async Task<IActionResult> GetOne(int id){var x=await db.Parts.FindAsync(id);return x is null?NotFound():Ok(x);}
 [Authorize(Roles="Admin"),HttpPost] public async Task<IActionResult> Create(PartItem x){x.Id=0;db.Parts.Add(x);try{await db.SaveChangesAsync();}catch(DbUpdateException){return Conflict(new{message="SKU already exists or data conflicts."});}return CreatedAtAction(nameof(GetOne),new{id=x.Id},x);}
 [Authorize(Roles="Admin"),HttpPut("{id:int}")] public async Task<IActionResult> Update(int id,PartItem input){var x=await db.Parts.FindAsync(id);if(x is null)return NotFound();x.Name=input.Name;x.SKU=input.SKU;x.Price=input.Price;x.Stock=input.Stock;x.IsActive=input.IsActive;try{await db.SaveChangesAsync();}catch(DbUpdateException){return Conflict(new{message="SKU already exists or data conflicts."});}return Ok(x);}
 [Authorize(Roles="Admin"),HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id){var x=await db.Parts.FindAsync(id);if(x is null)return NotFound();x.IsActive=false;await db.SaveChangesAsync();return NoContent();}
}
