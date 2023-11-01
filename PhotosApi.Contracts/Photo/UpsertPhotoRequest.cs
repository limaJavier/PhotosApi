namespace PhotosApi.Contracts.Photo;

public record UpsertPhotoRequest
(
    string Name,
    string Description
);