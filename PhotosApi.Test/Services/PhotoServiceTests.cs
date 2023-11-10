using Moq;
using PhotosApi.Models;
using PhotosApi.Repositories;
using PhotosApi.Services;

namespace PhotosApi.Tests.Services;

public class PhotosServiceTests
{
    private static PhotosApi.Repositories.MockRepository _repo = new PhotosApi.Repositories.MockRepository();

    [Theory]
    [InlineData("Some Name", "Some Description")]
    [InlineData("Some Name", "")]
    [InlineData("", "Some description")]
    [InlineData("", "")]
    [InlineData("Testing", "GetPhoto")]
    public void GetPhoto_ValidCall(string expectedName, string expectedDescription)
    {
        // Arrange
        BuildRepo();
        var expectedId = Guid.NewGuid();
        _repo.Store(new Photo { Id = expectedId, Name = expectedName, Description = expectedDescription });
        var repositoryMock = new Mock<IPhotosRepository>();
        repositoryMock.Setup(x => x.Get(expectedId)).Returns(_repo.Get(expectedId));

        var service = new PhotosService(repositoryMock.Object);

        // Act
        var result = service.GetPhoto(expectedId);

        // Assert
        Assert.Equal(expectedName, result.Name);
        Assert.Equal(expectedId, result.Id);
        Assert.Equal(expectedDescription, result.Description);
    }

    [Theory]
    [InlineData("Pic1", "Some description")]
    public void StorePhoto_ValidCall(string expectedName, string expectedDescription)
    {
        // Arrange
        BuildRepo();
        var expectedId = Guid.NewGuid();
        var repositoryMock = new Mock<IPhotosRepository>();
        var photo = new Photo { Id = expectedId, Name = expectedName, Description = expectedDescription };
        repositoryMock.Setup(x => x.Store(photo));

        var service = new PhotosService(repositoryMock.Object);

        // Act
        service.StorePhoto(photo);

        // Assert
        repositoryMock.Verify(x => x.Store(photo), Times.Exactly(1));
    }

    private void BuildRepo()
    {
        for (int i = 0; i < 100; i++)
            _repo.Store(new Photo { Id = Guid.NewGuid(), Description = $"Some description {i}", Name = $"Some name {i}" });
    }
}