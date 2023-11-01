using Microsoft.AspNetCore.Mvc;
using PhotosApi.Contracts.Photo;
using PhotosApi.Models;

namespace PhotosApi.Controllers;

[ApiController]
[Route("photos/")]
public class PhotosController : ControllerBase
{
    [HttpPost]
    public IActionResult StorePhoto(CreatePhotoRequest request)
    {
        // TODO
        // Store picture
        string url = "https:/fakeurl";

        var photo = new Photo
            (
                Guid.NewGuid(),
                request.Name,
                request.Description,
                DateTime.UtcNow,
                url
            );

        var response = new PhotoResponse
            (
                photo.Id,
                photo.Name,
                photo.Description,
                photo.LastModifiedDateTime,
                photo.Url
            );

        // This methods returns the Location we defined in EndPointsDefintion
        return CreatedAtAction
            (
                actionName: nameof(GetPhoto),
                routeValues: new { id = photo.Id },
                value: response
            );
    }

    public IActionResult GetPhoto()
    {
        return Ok();
    }
}