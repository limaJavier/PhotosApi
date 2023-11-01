namespace PhotosApi.Contracts.Photo;

public record CreatePhotoRequest
(
    string Name,
    string Description,
    IFormFile File
);