namespace PhotosApi.Contracts.Photo;

public record PhotoResponse
(
    Guid id,
    string Name,
    string Description,
    DateTime LastModifiedDateTime,
    string Url
);