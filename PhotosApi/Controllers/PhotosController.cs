using Microsoft.AspNetCore.Mvc;

namespace PhotosApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhotosController : ControllerBase
{
    // GET: api/<PhotosController>
    [HttpGet]
    public IEnumerable<string> GetAllPhotos()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<PhotosController>/5
    [HttpGet("{id}")]
    public string GetPhoto(int id)
    {
        return "value";
    }

    // POST api/<PhotosController>
    [HttpPost]
    public void StorePhoto([FromBody] string value)
    {
    }

    // PUT api/<PhotosController>/5
    [HttpPut("{id}")]
    public void ChangePhoto(int id, [FromBody] string value)
    {
    }

    // DELETE api/<PhotosController>/5
    [HttpDelete("{id}")]
    public void DeletePhoto(int id)
    {
    }
}