using Microsoft.AspNetCore.Mvc;
using PhotosApi.Contracts.Photo;
using PhotosApi.Models;
using PhotosApi.Services;

namespace PhotosApi.Controllers;

[ApiController]
[Route("photos/")]
public class PhotosController : ControllerBase
{
    private IPhotosService _photosService;
    public PhotosController(IPhotosService photosService)
    {
        _photosService = photosService;
    }

    [HttpPost]
    public IActionResult StorePhoto([FromForm] CreatePhotoRequest request)
    {
        // TODO
        // Store picture

        
        string url = "https://fakeurl";

        var photo = new Photo
            (
                Guid.NewGuid(),
                request.Name,
                request.Description,
                DateTime.UtcNow,
                url
            );

        _photosService.StorePhoto(photo);

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

    [HttpGet("{id:guid}")]
    public IActionResult GetPhoto(Guid id)
    {
        var photo = _photosService.GetPhoto(id);

        var response = new PhotoResponse
           (
               photo.Id,
               photo.Name,
               photo.Description,
               photo.LastModifiedDateTime,
               photo.Url
           );

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertPhoto(Guid id, UpsertPhotoRequest request)
    {
        // TODO
        // Get Url from IStorage
        string url = "https://fakeurl";

        var photo = new Photo
            (
                id,
                request.Name,
                request.Description,
                DateTime.UtcNow,
                url
            );

        _photosService.UpsertPhoto(photo);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeletePhoto(Guid id)
    {
        _photosService.DeletePhoto(id);

        return NoContent();
    }

    [HttpGet("/download/{id:guid}")]
    public IActionResult DownloadPhoto(Guid id)
    {
        _photosService.DeletePhoto(id);
        return NoContent();
    }

    [HttpGet]
    public IActionResult GetPhotos()
    {
        return Ok(_photosService.GetPhotos());
    }
}