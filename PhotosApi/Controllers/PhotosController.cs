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
    private IStorageService _storageService;
    public PhotosController(IPhotosService photosService, IStorageService storageService)
    {
        _photosService = photosService;
        _storageService = storageService;
    }

    [HttpPost]
    public IActionResult StorePhoto([FromForm] CreatePhotoRequest request)
    {
        var id = Guid.NewGuid();
        var url = _storageService.StorePhoto(id, request.File);

        var photo = new Photo
            (
                id,
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
        _storageService.DeletePhoto(id);
        _photosService.DeletePhoto(id);

        return NoContent();
    }

    [HttpGet("/storage/{id:guid}")]
    public IActionResult DownloadPhoto(Guid id)
    {
        var fileData = _storageService.DownloadPhoto(id);
        return File(fileData.Item1, fileData.Item2);
    }

    [HttpGet]
    public IActionResult GetPhotos()
    {
        return Ok(_photosService.GetPhotos());
    }
}