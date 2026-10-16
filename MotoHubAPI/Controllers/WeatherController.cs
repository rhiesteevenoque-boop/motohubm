using System.Text.Json; using Microsoft.AspNetCore.Mvc;
namespace MotoHubAPI.Controllers;
[ApiController,Route("api/weather")]
public class WeatherController(IHttpClientFactory factory):ControllerBase {
 // External API integration: Open-Meteo, no API key required. Coordinates are supplied by the client.
 [HttpGet] public async Task<IActionResult> Get([FromQuery] double latitude=8.35,[FromQuery] double longitude=124.87){if(latitude is <-90 or >90||longitude is <-180 or >180)return BadRequest(new{message="Latitude/longitude out of range."});try{var client=factory.CreateClient();client.Timeout=TimeSpan.FromSeconds(8);var url=$"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature_2m,relative_humidity_2m,precipitation,weather_code,wind_speed_10m";using var response=await client.GetAsync(url);if(!response.IsSuccessStatusCode)return StatusCode(502,new{message="Weather provider returned an error."});using var doc=JsonDocument.Parse(await response.Content.ReadAsStringAsync());return Ok(new{source="Open-Meteo",latitude,longitude,current=doc.RootElement.GetProperty("current")});}catch(TaskCanceledException){return StatusCode(504,new{message="Weather provider timed out."});}catch(HttpRequestException){return StatusCode(502,new{message="Weather provider is unavailable."});}}
}
