using Microsoft.AspNetCore.Mvc;

namespace earfest.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    // GET: api/Values
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new string[] { "value1", "value2" });
    }
    // GET: api/Values/5
    [HttpGet("{id}", Name = "Get")]
    public IActionResult Get(int id)
    {
        return Ok("value");
    }
    // POST: api/Values
    [HttpPost]
    public IActionResult Post([FromBody] string value)
    {
        return Ok();
    }
    // PUT: api/Values/5
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] string value)
    {
        return Ok();
    }
    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return Ok();
    }
}
